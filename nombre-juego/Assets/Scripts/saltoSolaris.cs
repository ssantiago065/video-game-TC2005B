using UnityEngine; //libreria necesaria para usar scripts en unity
using System.Collections;
using System.Collections.Generic;

public class saltoSolaris : MonoBehaviour
{
    public float speed = 5f; //velocidad de movimiento del personaje punto flotante
    public float jumpForce = 4f; // cantidad de fuerza del salto
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isGrounded; //bandera para saber si está en el piso.
    public bool cooldownDash = true;
    private float fuerzaDash = 20f;
    private float duracionDash = 0.5f;
    public bool isDashing = false;
    private TrailRenderer tr;
    private float direccionDash = 1f;
    private SpriteRenderer spriteRenderer;
    private bool orientaDer = true;
    public saltoAeterius aeterius;
    private float velocidadWallSlide = 2f;
    [SerializeField] private LayerMask capaPared;
    [SerializeField] private Transform checkPared;
    controladorAudio controladorAudio;

    private void Awake()
    {
        controladorAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<controladorAudio>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //variable que almacena un rigibody
        tr = GetComponent<TrailRenderer>();
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

            direccionDash = moveInput.x;
            if (direccionDash == 0)
                direccionDash = transform.localScale.x;

            float gravedadOriginal = rb.gravityScale;
            rb.gravityScale = 0f;

            rb.linearVelocity = new Vector2(direccionDash * fuerzaDash, 0f);
            tr.emitting = true;

            float tiempo = 0f;
            float intervaloGolpe = 0.05f;
            controladorAudio.PlaySFX(controladorAudio.Dash);
            while (tiempo < duracionDash)
            {
                GetComponent<ataqueSolaris>().Attack();

                tiempo += intervaloGolpe;
                yield return new WaitForSeconds(intervaloGolpe);
            }

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

            if (moveInput.x > 0 && !orientaDer)
            {
                Flip();
            }
            else if (moveInput.x < 0 && orientaDer)
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
            controladorAudio.PlaySFX(controladorAudio.Salto);
            isGrounded = false;
        }
        else
        {
            StartCoroutine(dash());
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -velocidadWallSlide, float.MaxValue));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el personaje está tocando el suelo
        if (collision.gameObject.CompareTag("suelo") || collision.gameObject.CompareTag("enemigo"))
        {
            isGrounded = true;
            cooldownDash = true;
            aeterius.cooldownSalto = true;
        }
    }
}