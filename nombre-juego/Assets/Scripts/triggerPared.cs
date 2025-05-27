using UnityEngine;

public class paredTrigger : MonoBehaviour
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