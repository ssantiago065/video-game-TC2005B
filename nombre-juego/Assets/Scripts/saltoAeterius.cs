using UnityEngine; //libreria necesaria para usar scripts en unity

public class saltoAeterius : MonoBehaviour
{
    public float speed = 5f; //velocidad de movimiento del personaje punto flotante
    public float jumpForce = 10f; // cantidad de fuerza del salto
    private Rigidbody2D rb;
    private Vector2 moveInput; 
    public bool isGrounded; //bandera para saber si está en el piso.
    public bool cooldownSalto = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //variable que almacena un rigibody
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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        //rb.linearVelocity = new Vector2(rb.linearVelocity.x ,moveInput.y * speed);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el personaje está tocando el suelo
        if (collision.gameObject.CompareTag("suelo"))
        {
            isGrounded = true;
            cooldownSalto = true;
        }
    }

}