using UnityEngine;
using System.Collections;

/// <summary>
/// Main class to hold player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    public float walkSpeed = 1f;
    public float runSpeed = 2f;
    public bool canRun = true;
    public bool canMove = true;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public bool canJump = true;
    [Range(0f, 1f)] public float controlInAirMultiplier = 1f;
    [SerializeField] private LayerMask groundLayer;
    public float doubleJumpTime = 0f;

    [Header("Combat")]
    public int damage = 2;

    private float horizontalMove;
    private float lastAngle = 0f;
    private bool isSprint = false;
    private bool doubleJumped = false;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    
    public float CurrentSpeed { get; private set; }
    public bool IsGrounded { get; private set; }

    public static GameObject CurrentPlayer { get; private set; }

    private void Awake() => CurrentPlayer = gameObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            CheckGround();
            SetDirection();
            AnimationsController();

            if(Input.GetKeyUp(KeyCode.Escape)) 
            {   
                PauseScreen.Screen.GetComponent<PauseScreen>().Pause();
            }  
        }

        if(IsGrounded) 
        {
            doubleJumped = false;
        }

        if(doubleJumpTime > 0)
        {
            doubleJumpTime -= Time.deltaTime;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal");
        isSprint = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded || (doubleJumpTime > 0 && !doubleJumped)) && canJump && canMove)
        {
            Jump();
        }
    }

    public void AddDoubleJumpTime(float seconds) => doubleJumpTime += seconds;

    /// <summary>
    /// Basic jumping.
    /// </summary>
    private void Jump ()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("jump");
        anim.SetBool("land", false);
    }

    /// <summary>
    /// Rotates the player towards movement direction.
    /// </summary>
    private void SetDirection ()
    {
        if (horizontalMove != 0)
        {
            float angle = Mathf.Atan2(0, horizontalMove) * Mathf.Rad2Deg;
            lastAngle = angle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(lastAngle, Vector3.up);
        }
    }

    /// <summary>
    /// Triggers when player gets hit by enemy.
    /// </summary>
    public void GetHit() => anim.SetTrigger("getHit");

    public void Death ()
    {
        anim.enabled = false;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        rb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
        col.enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;
        canMove = false;
        StartCoroutine(WaitForGameOver());
    }

    private IEnumerator WaitForGameOver ()
    {
        yield return new WaitForSecondsRealtime(2f);
        GameOver.EndGame();
    }

    /// <summary>
    /// Checks is player touching ground.
    /// </summary>
    private void CheckGround ()
    {
        Vector2 dir = new Vector2(0, -1);
        float length = 0.08f;

        if (!IsGrounded && 
            Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2)), new Vector2(0, -1), 0.1f, groundLayer))
        {
            anim.SetBool("land", true);
        }

        RaycastHit2D hit1 = 
            Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2)), dir, length, groundLayer);

        RaycastHit2D hit2 =
            Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 4), transform.position.y - (transform.localScale.y / 2)), dir, length, groundLayer);

        RaycastHit2D hit3 =
            Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 4), transform.position.y - (transform.localScale.y / 2)), dir, length, groundLayer);

        if (hit1 || hit2 || hit3)
        {
            IsGrounded = true;
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
            IsGrounded = false;
        }
    }

    /// <summary>
    /// Holds player movement.
    /// </summary>
    private void FixedUpdate()
    {
        CurrentSpeed = isSprint && canRun ? runSpeed : walkSpeed;
        CurrentSpeed *= !IsGrounded ? controlInAirMultiplier : 1f;
        CurrentSpeed *= canMove ? 1f : 0f;
        rb.velocity = new Vector2(horizontalMove * CurrentSpeed, rb.velocity.y);
    }

    private void AnimationsController ()
    {
        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            if (CurrentSpeed == walkSpeed)
            {
                anim.SetBool("sprint", false);
                anim.SetBool("walk", true);
            }
            else if (CurrentSpeed == runSpeed)
            {
                anim.SetBool("sprint", true);
                anim.SetBool("walk", false);
            }
        }
        else
        {
            anim.SetBool("sprint", false);
            anim.SetBool("walk", false);
        }
    }
}
