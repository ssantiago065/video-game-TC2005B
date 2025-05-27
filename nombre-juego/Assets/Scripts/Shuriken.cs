using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float velocidad = 10f;
    public Vector2 direccion = Vector2.right;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direccion.normalized * velocidad;  // Nota: es velocity, no linearVelocity
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemigo"))
        {
            Debug.Log("Enemigo golpeado");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("suelo") || collision.gameObject.CompareTag("pared"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        logicaDron dron = other.GetComponent<logicaDron>();
        if (dron != null)
        {
            dron.DetenerTemporalmente(10f);
            Destroy(gameObject); 
        }
    }
}
