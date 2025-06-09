using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerRompible : MonoBehaviour
{
    public enum TipoTrigger { Etapa2, Etapa3, Boss }
    public TipoTrigger tipo;
    public controladorEscena controlador;

    public void Activar()
    {
        switch (tipo)
        {
            case TipoTrigger.Etapa2:
                controlador.ActivarEtapa2();
                Destroy(gameObject, 2f);
                break;

            case TipoTrigger.Etapa3:
                controlador.ActivarEtapa3();
                Destroy(gameObject, 2f);
                break;

            case TipoTrigger.Boss:
                StartCoroutine(FinPartidaConFade());
                break;
        }
    }

    private IEnumerator FinPartidaConFade()
    {
        EstadoJugador.esInvencible = true;
        EstadoJugador.inputBloqueado = true;

        CambioPersonaje cambio = FindFirstObjectByType<CambioPersonaje>();
        if (cambio != null)
        {
            Transform personaje = cambio.ObtenerPersonajeActivo();
            Rigidbody2D rb = personaje.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        yield return controlador.StartCoroutine(controlador.Fade(0f, 1f));

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1f;

        SceneManager.LoadScene(3);
    }
}
