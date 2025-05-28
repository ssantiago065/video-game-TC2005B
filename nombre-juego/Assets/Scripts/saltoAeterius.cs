using UnityEngine; //libreria necesaria para usar scripts en unity

public class saltoAeterius : MonoBehaviour
{
    public float speed = 5f; //velocidad de movimiento del personaje punto flotante
    public float jumpForce = 4f; // cantidad de fuerza del salto
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isGrounded; //bandera para saber si está en el piso.
    public bool cooldownSalto = true;
    private SpriteRenderer spriteRenderer;
    private bool orientaDer = true;
    public saltoSolaris solaris;
    private bool isWallSliding;
    private float velocidadWallSlide = 2f;
    [SerializeField] private LayerMask capaPared;
    [SerializeField] private Transform checkPared;
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
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

            if (moveInput.x > 0 && !orientaDer && !isWallJumping)
            {
                Flip();
            }
            else if (moveInput.x < 0 && orientaDer && !isWallJumping)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        orientaDer = !orientaDer;
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

    void dobleSalto()
    {
        if (cooldownSalto && !isWalled())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //cooldownSalto = false;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(checkPared.position, 0.2f, capaPared);
    }

    private void wallSlide()
    {
        if (isWalled() && !isGrounded && moveInput.x != 0f)
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
        if (collision.gameObject.CompareTag("suelo") || collision.gameObject.CompareTag("enemigo"))
        {
            isGrounded = true;
            cooldownSalto = true;
            solaris.cooldownDash = true;
        }
    }
    
}