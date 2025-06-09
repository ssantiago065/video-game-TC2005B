using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisionó tiene el tag "jugador"
        if (collision.collider.CompareTag("jugador"))
        {
            Debug.Log("Jugador colisionó con la Zona de Muerte");

            // Buscar el componente de vida en el padre del personaje activo
            Vida vidaJugador = collision.collider.GetComponentInParent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.SetCurrentHealth(0);
                vidaJugador.Die();// Mata al jugador directamente
            }
        }
    }
}