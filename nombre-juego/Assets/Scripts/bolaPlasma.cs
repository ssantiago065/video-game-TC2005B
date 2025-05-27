using UnityEngine;

public class bolaPlasma : MonoBehaviour
{
    public float velocidad = 5f;
    public Vector2 direccion = Vector2.right;

    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.linearVelocity = direccion.normalized * velocidad;

        GameObject[] bolas = GameObject.FindGameObjectsWithTag("bola");

        foreach (GameObject bola in bolas)
        {
            if (bola != this.gameObject) // para no ignorar colisión con sí misma
            {
                Collider2D colOtraBola = bola.GetComponent<Collider2D>();
                if (colOtraBola != null)
                {
                    Physics2D.IgnoreCollision(col, colOtraBola);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jugador"))
        {
            Debug.Log("Jugador golpeado");
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
        else if (collision.gameObject.CompareTag("techo"))
        {
            Destroy(gameObject);
        }
    }
}
