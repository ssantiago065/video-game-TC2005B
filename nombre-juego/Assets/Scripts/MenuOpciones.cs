using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuOpciones : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderSFX;
    private controladorAudio audioControl;

    private void Start()
    {
        audioControl = FindFirstObjectByType<controladorAudio>();

        float volMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
        float volSFX = PlayerPrefs.GetFloat("VolumenSFX", 0.5f);

        sliderMusica.value = volMusica;
        sliderSFX.value = volSFX;

        audioControl.CambiarVolumenMusica(volMusica);
        audioControl.CambiarVolumenSFX(volSFX);
    }

    public void CambiarVolumenMusica(float valor)
    {
        audioControl.CambiarVolumenMusica(valor);
    }

    public void CambiarVolumenSFX(float valor)
    {
        audioControl.CambiarVolumenSFX(valor);
    }

    public void BorrarDatosGuardado()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "datosGuardado.json");
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
