using UnityEngine; //libreria necesaria para usar scripts en unity
using System.Collections;
using System.Collections.Generic;

public class saltoSolaris : MonoBehaviour
{
    public float speed = 5f; //velocidad de movimiento del personaje punto flotante
    public float jumpForce = 10f; // cantidad de fuerza del salto
    private Rigidbody2D rb;
    private Vector2 moveInput; 
    public bool isGrounded; //bandera para saber si está en el piso.
    public bool cooldownDash = true;
    private float fuerzaDash = 10f;
    private float duracionDash = 0.5f;
    public bool isDashing = false;
    private TrailRenderer tr;
    private float direccionDash = 1f;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    public saltoAeterius aeterius;
    private float velocidadWallSlide = 2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //variable que almacena un rigibody
        tr = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        //moveInput.y = Input.GetAxis("Vertical");

        // Detectar salto con barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        wallSlide();
    }

    private IEnumerator dash()
    {
        if (cooldownDash)
        {
            cooldownDash = false;
            isDashing = true;

            GetComponent<ataqueSolaris>().Attack();
            direccionDash = moveInput.x;
            if (direccionDash == 0)
                direccionDash = transform.localScale.x;

            float gravedadOriginal = rb.gravityScale;
            rb.gravityScale = 0f;

            rb.linearVelocity = new Vector2(direccionDash * fuerzaDash, 0f);
            tr.emitting = true;

            yield return new WaitForSeconds(duracionDash);

            tr.emitting = false;
            rb.gravityScale = gravedadOriginal;
            isDashing = false;
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

            if (moveInput.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput.x < 0 && facingRight)
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
            StartCoroutine(dash());
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void wallSlide()
    {
        if (isWalled() && !isGrounded && moveInput.x != 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -velocidadWallSlide, float.MaxValue));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el personaje está tocando el suelo
        if (collision.gameObject.CompareTag("suelo"))
        {
            isGrounded = true;
            cooldownDash = true;
            aeterius.cooldownSalto = true;
        }
    }

}