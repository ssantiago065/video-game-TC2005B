using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class triggerBoss : MonoBehaviour
{
    public controladorEscena controlador; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jugador"))
        {
            StartCoroutine(FinPartidaConFade());
        }
    }

    IEnumerator FinPartidaConFade()
    {
        // Fade to black
        yield return controlador.StartCoroutine(controlador.Fade(0f, 1f));

        yield return new WaitForSeconds(0.5f); // opcional, dramatismo

        // Cargar escena del menú (índice 0)
        SceneManager.LoadScene(2);
    }
}
