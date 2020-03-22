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
    
    private float horizontalMove;
    private bool isSprint = false;

    private Rigidbody2D rb;
    
    public float CurrentSpeed { get; private set; }
    public bool IsGrounded { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();

        horizontalMove = Input.GetAxisRaw("Horizontal");
        isSprint = Input.GetKey(KeyCode.LeftShift);

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded && canJump)
        {
            Jump();
        }
    }

    /// <summary>
    /// Basic jumping.
    /// </summary>
    private void Jump ()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Checks is player touching ground.
    /// </summary>
    private void CheckGround ()
    {
        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2)), new Vector2(0, -1), 0.05f))
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
