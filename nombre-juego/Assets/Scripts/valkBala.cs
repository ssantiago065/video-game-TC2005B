using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 5f;
    private Vector2 direccion;
    public float damageToPlayer = 10f;

    public void SetDireccion(Vector2 dir)
    {
        direccion = dir.normalized;

        // Rotar el sprite hacia la dirección de movimiento
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("jugador"))
        {
            Vida vidaJugador = other.GetComponent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("limites"))
        {
            Destroy(gameObject);
        }
    }

}
