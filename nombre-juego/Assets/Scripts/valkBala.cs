using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 5f;
    private Vector2 direccion;
    public float damageToPlayer = 10f;

    public void SetDireccion(Vector2 dir)
    {
        direccion = dir.normalized;

        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("jugador"))
        {

            ModificadorDaño mod = other.GetComponent<ModificadorDaño>();
            float multiplicador = (mod != null) ? mod.multiplicadorDaño : 1f;

            Vida vidaJugador = other.GetComponentInParent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.TakeDamage(10f * multiplicador);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("suelo") || other.CompareTag("pared") || other.CompareTag("techo") || other.CompareTag("Limites"))
        {
            Destroy(gameObject);
        }
    }
}