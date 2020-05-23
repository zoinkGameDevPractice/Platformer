using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private CharacterCombat cc;
    private CharacterStats myStats;
    public Transform interactionDetection;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumpValue;
    public int extraJumps;

    private float groundTimer;
    public float rememberGround;
    private float jumpTimer;
    public float rememberJump;

    public float interactionDistance = 2f;
    private bool facingRight = false;

    private Equipment currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        extraJumpValue = extraJumps;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CharacterCombat>();
        myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        groundTimer -= Time.deltaTime;
        jumpTimer -= Time.deltaTime;
        CheckForJump();
        CheckForGroundTime();

        if(isGrounded == true)
        {
            extraJumpValue = extraJumps;
        }
        moveInput = Input.GetAxisRaw("Horizontal");

        CheckForInteraction();

        if (facingRight == false && moveInput > 0)
            Flip();
        else if (facingRight && moveInput < 0)
            Flip();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void CheckForJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = rememberJump;
        }

        if (Input.GetButtonDown("Jump") && extraJumpValue > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumpValue--;
        }
        else if (jumpTimer > 0 && extraJumpValue == 0 && groundTimer > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void CheckForGroundTime()
    {
        if(isGrounded)
        {
            groundTimer = rememberGround;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = null;
        }
    }

    void CheckForInteraction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D ray = Physics2D.Raycast(interactionDetection.position, Vector2.right, interactionDistance);
            if(ray.collider)
            {
                Interactable interactable = ray.collider.gameObject.GetComponent<Interactable>();
                if (interactable)
                {
                    interactable.PickUp();
                }
                else return;
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {
            cc.PreAttack(currentWeapon, PlayerManager.instance.point);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(interactionDetection.position, Vector2.right);
        Gizmos.DrawWireSphere(interactionDetection.position, interactionDistance);
    }

    public void GetWeapon(Equipment weapon)
    {
        currentWeapon = weapon;
    }

    /* Old interaction system
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.PickUp();
            }
        }
    }
    */
}
