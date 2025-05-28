using UnityEngine;

public class triggerParedFinal : MonoBehaviour
{
    public controladorEscena controlador;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jugador"))
        {
            controlador.ActivarEtapa3();
            Destroy(gameObject, 2f);
        }
    }
}