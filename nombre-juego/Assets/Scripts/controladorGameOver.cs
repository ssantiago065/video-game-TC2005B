using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGameOver : MonoBehaviour
{
    public void Reintentar()
    {
        MenuPausa.EsGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenu()
    {
        MenuPausa.EsGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}