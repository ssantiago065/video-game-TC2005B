using UnityEngine;

public class saltoAeterius : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 4f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isGrounded;
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
    private Vector2 fuerzaWallJump = new Vector2(20f, 25f);
    controladorAudio controladorAudio;

    private void Awake()
    {
        controladorAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<controladorAudio>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (EstadoJugador.inputBloqueado)
        {
            moveInput.x = 0f;
            return;
        }
        moveInput.x = Input.GetAxis("Horizontal");

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

    void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            controladorAudio.PlaySFX(controladorAudio.Salto);
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
            controladorAudio.PlaySFX(controladorAudio.dobleSalto);
            cooldownSalto = false;
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
        if (collision.gameObject.CompareTag("suelo") || collision.gameObject.CompareTag("enemigo"))
        {
            isGrounded = true;
            cooldownSalto = true;
            solaris.cooldownDash = true;
        }
    }
    
}