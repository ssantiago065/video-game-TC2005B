using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void JugarJuego()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
