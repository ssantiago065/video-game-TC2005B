using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float velocidad = 10f;
    public Vector2 direccion = Vector2.right;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direccion.normalized * velocidad;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemigo"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("suelo") || collision.gameObject.CompareTag("pared") || collision.gameObject.CompareTag("Limites"))
        {
            Destroy(gameObject);
        }
    }
}
