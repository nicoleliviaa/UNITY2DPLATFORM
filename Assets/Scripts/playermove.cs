using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer; // Ensure this includes the platform layer
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    [SerializeField] private Vector2 wallCheckOffset = new Vector2(0.5f, 0f);
    [SerializeField] private Vector2 wallCheckSize = new Vector2(0.1f, 1.0f);
    private Animator anim;
    public CoinManager cm;
    private Timer timer;
    public ShootingItem shootingItem;
    private Rigidbody2D body;
    private bool grounded;
    private bool touchingWall;
    private bool facingRight = true;

    private Transform groundCheck;
    private Transform wallCheck;

    private float horizontalInput; // Declare horizontalInput at the class level
    public GameManagerScript gameManagerScript;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Create groundCheck and wallCheck objects
        groundCheck = new GameObject("GroundCheck").transform;
        wallCheck = new GameObject("WallCheck").transform;

        // Set them as children of the player
        groundCheck.parent = transform;
        wallCheck.parent = transform;

        // Position them relative to the player
        groundCheck.localPosition = groundCheckOffset;
        wallCheck.localPosition = wallCheckOffset;
        timer = FindObjectOfType<Timer>();
        cm = FindObjectOfType<CoinManager>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Check if touching a wall and prevent movement towards the wall
        if (touchingWall && horizontalInput < 0 && facingRight || touchingWall && horizontalInput > 0 && !facingRight)
        {
            horizontalInput = 0;
        }

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when facing left/right
        if (horizontalInput > 0.01f && !facingRight)
        {
            Flip();
           
        }
        else if (horizontalInput < -0.01f && facingRight)
            Flip();
      

        // Jump when space key is pressed and the character is grounded
        if (Input.GetButtonDown("Jump") && grounded)
            Jump();

        // Attack when the 'Fire1' button (default is left Ctrl or mouse left click) is pressed
        if (Input.GetButtonDown("Fire1") && canAttack())
            Attack();

        // Set animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void Attack()
    {
        Debug.Log("Attack triggered"); // Debugging attack trigger
        anim.SetTrigger("attack");
    }

    private void FixedUpdate()
    {
        // Perform ground check using CircleCast
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer); // Increased radius for better detection
        Debug.Log("Grounded: " + grounded + ", GroundCheck Position: " + groundCheck.position); // Debugging ground detection

        // Check if touching a wall using BoxCast
        touchingWall = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0f, Vector2.zero, 0f, wallLayer);
        Debug.Log("Touching Wall: " + touchingWall + ", WallCheck Position: " + wallCheck.position); // Debugging wall detection
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check and wall check in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)groundCheckOffset, 0.3f); // Increased radius for visualization

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (Vector3)wallCheckOffset, new Vector3(wallCheckSize.x, wallCheckSize.y, 1.0f));
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;

        // Flip the wall check offset to the other side
        wallCheckOffset.x *= -1;
        wallCheck.localPosition = wallCheckOffset;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private bool isGrounded()
    {
        return grounded;
    }

    private bool onWall()
    {
        return touchingWall;
    }
    public void LoadFinish()
    {
        SceneManager.LoadScene("FinishScene"); // Make sure the scene name matches the name of your Level 1 scene
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coins")) {
            Destroy(other.gameObject);
            cm.coincount++;
        }
        else if (other.gameObject.CompareTag("Pitfall") || other.gameObject.CompareTag("Enemy"))
        {
            gameManagerScript.gameOver();
        }else if (other.gameObject.CompareTag("Finish") && timer.GetCurrentTime() > 0 && cm.GetCoinCount()>=20)
        {
            SceneManager.LoadScene("FINISHSCREEN");
        }
        else if (timer.GetCurrentTime() <= 0)
        {
            gameManagerScript.gameOver();  // Call Die() if the timer reaches 0
        }
    }


    
   
}

