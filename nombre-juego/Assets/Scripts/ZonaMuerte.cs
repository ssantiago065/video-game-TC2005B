using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("jugador"))
        {
            Vida vidaJugador = collision.collider.GetComponentInParent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.SetCurrentHealth(0);
                vidaJugador.Die();
            }
        }
    }
}