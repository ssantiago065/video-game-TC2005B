using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class MenuPrincipal : MonoBehaviour
{
    public void JugarJuego()
    {
        string path = Path.Combine(Application.persistentDataPath, "datosGuardado.json");

        if (!File.Exists(path))
        {
            SceneManager.LoadScene(1);
            return;
        }

        string json = File.ReadAllText(path);
        datosGuardado datos = JsonUtility.FromJson<datosGuardado>(json);

        PlayerPrefs.SetInt("CargarGuardado", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(datos.indiceNivel);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
