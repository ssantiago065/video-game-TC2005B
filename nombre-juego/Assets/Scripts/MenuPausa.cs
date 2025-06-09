using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public static bool JuegoPausado = false;
    public static bool EsGameOver = false; 

    public GameObject menuPausaUI;
    public GameObject menuOpcionesUI;

    void Update()
    {
        if (EsGameOver) return; 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JuegoPausado)
            {
                Resumir();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Resumir()
    {
        menuPausaUI.SetActive(false);
        menuOpcionesUI.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        JuegoPausado = true;
    }

    public void CargarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
