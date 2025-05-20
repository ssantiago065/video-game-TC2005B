using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.right;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * speed;
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
    }
}