using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public int maxJumps = 2;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;

    private int jumpCount = 0;
    private bool isGrounded;

    public PlayerHealth health;

    [Header("Distance Tracking")]
    public float distanceCovered = 0f;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        startPosition = transform.position;
    }

    void Update()
    {
        GroundCheck();
        HandleInput();
        UpdateAnimation();
        TrackDistance();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TryJump();
        }
    }

    void TryJump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    void UpdateAnimation()
    {
        anim.SetBool("isJumping", !isGrounded);
        anim.SetInteger("jumpCount", jumpCount);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(1);
        }

        if(collision.gameObject.CompareTag("Coin"))
        {
            UIController.Instance.IncremententScore();
            UIController.Instance.UpdateScore();
            Destroy(collision.gameObject);
        }
    }

    void TrackDistance()
    {
        distanceCovered = transform.position.x - startPosition.x;
    }

    public float GetDistanceCovered()
    {
        return distanceCovered;
    }
}