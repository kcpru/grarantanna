using UnityEngine;

/// <summary>
/// Main class to hold player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    public float walkSpeed = 1f;
    public float runSpeed = 2f;
    public bool canRun = true;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public bool canJump = true;
    [Range(0f, 1f)] public float controlInAirMultiplier = 1f;
    [SerializeField] private LayerMask groundLayer;
    
    private float horizontalMove;
    private float lastAngle = 0f;
    private bool isSprint = false;

    private Rigidbody2D rb;
    private Collider2D col;
    
    public float CurrentSpeed { get; private set; }
    public bool IsGrounded { get; private set; }

    public static GameObject CurrentPlayer { get; private set; }

    private void Awake() => CurrentPlayer = gameObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckGround();
        SetDirection();

        horizontalMove = Input.GetAxisRaw("Horizontal");
        isSprint = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && canJump)
        {
            Jump();
        }
    }

    /// <summary>
    /// Basic jumping.
    /// </summary>
    private void Jump ()
    {
        rb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
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
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(lastAngle, Vector3.forward);
        }
    }

    /// <summary>
    /// Checks is player touching ground.
    /// </summary>
    private void CheckGround ()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2)), new Vector2(0, -1), 0.01f, groundLayer))
        {
            IsGrounded = true;
        }
        else
        {
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
        rb.velocity = new Vector2(horizontalMove * CurrentSpeed, rb.velocity.y);
    }
}
