using UnityEngine;

public class Bullet : MonoBehaviour
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
            Debug.Log("Enemigo golpeado");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("suelo"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("pared"))
        {
            Destroy(gameObject); 
        }
    }
}