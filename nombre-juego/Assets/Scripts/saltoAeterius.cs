using UnityEngine; //libreria necesaria para usar scripts en unity

public class saltoAeterius : MonoBehaviour
{
    public float speed = 5f; //velocidad de movimiento del personaje punto flotante
    public float jumpForce = 6f; // cantidad de fuerza del salto
    private Rigidbody2D rb;
    private Vector2 moveInput; 
    public bool isGrounded; //bandera para saber si está en el piso.
    public bool cooldownSalto = true;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    public saltoSolaris solaris;
    private bool isWallSliding;
    private float velocidadWallSlide = 2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    private float horizontal;
    private bool isWallJumping;
    private float direccionWallJump;
    private float tiempoWallJump = 1f;
    private float contadorWallJump;
    private Vector2 fuerzaWallJump = new Vector2(8f, 10f);



    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //variable que almacena un rigibody
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        //moveInput.y = Input.GetAxis("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        // Detectar salto con barra espaciadora o clic del mouse
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        wallSlide();
        wallJump();

    }

    void FixedUpdate()
    {
        if (!isWallJumping)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

            if (moveX > 0 && !facingRight && !isWallJumping)
            {
                Flip();
            }
            else if (moveX < 0 && facingRight && !isWallJumping)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump() //cambio de bandera para verificar que esta en el suelo
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
        else
        {
            dobleSalto();
        }
    }

    void dobleSalto() {
        if (cooldownSalto)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            cooldownSalto = false;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void wallSlide()
    {
        if (isWalled() && !isGrounded && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -velocidadWallSlide, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            direccionWallJump = -transform.localScale.x;
            contadorWallJump = tiempoWallJump;

            CancelInvoke(nameof(stopWallJump));
        }
        else
        {
            contadorWallJump -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && contadorWallJump > 0f)
        {
            rb.linearVelocity = new Vector2(fuerzaWallJump.x * direccionWallJump, fuerzaWallJump.y);
            isWallJumping = true;
            contadorWallJump = 0f;

            if (transform.localScale.x != direccionWallJump)
            {
                Flip();
            }
            
            Invoke(nameof(stopWallJump), tiempoWallJump);
        }
    }

    private void stopWallJump()
    {
        isWallJumping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el personaje está tocando el suelo
        if (collision.gameObject.CompareTag("suelo"))
        {
            isGrounded = true;
            cooldownSalto = true;
            solaris.cooldownDash = true;
        }
    }

}