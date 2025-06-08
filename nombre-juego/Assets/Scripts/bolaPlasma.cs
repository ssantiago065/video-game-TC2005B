using UnityEngine;

public class bolaPlasma : MonoBehaviour
{
    public float velocidad = 5f;
    public Vector2 direccion = Vector2.right;

    private Rigidbody2D rb;
    private Collider2D col;
    public float damageToPlayer = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.linearVelocity = direccion.normalized * velocidad;

        GameObject[] bolas = GameObject.FindGameObjectsWithTag("bola");

        foreach (GameObject bola in bolas)
        {
            if (bola != this.gameObject)
            {
                Collider2D colOtraBola = bola.GetComponent<Collider2D>();
                if (colOtraBola != null)
                {
                    Physics2D.IgnoreCollision(col, colOtraBola);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("jugador"))
        {
            Debug.Log("Jugador golpeado");
            Vida playerHealth = other.GetComponent<Vida>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("suelo") || other.CompareTag("pared") || other.CompareTag("techo"))
        {
            Destroy(gameObject);
        }
    }
}
