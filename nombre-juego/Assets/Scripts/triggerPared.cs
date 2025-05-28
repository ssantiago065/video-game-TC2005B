using UnityEngine;

public class triggerPared : MonoBehaviour
{
    public controladorEscena controlador;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jugador"))
        {
            controlador.ActivarEtapa2();
        }
    }
}