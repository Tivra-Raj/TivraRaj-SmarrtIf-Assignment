using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGameRunning = false;
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

    void Start()
    {
        isGameRunning = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GroundCheck();
        HandleInput();
        UpdateAnimation();
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
        if(collision.gameObject.GetComponent<EnemyController>() != null)
        {
            isGameRunning = false;
        }

        if(collision.gameObject.CompareTag("Coin"))
        {
            UIController.Instance.IncremententScore();
            UIController.Instance.UpdateScore();
            Destroy(collision.gameObject);
        }
    }
}