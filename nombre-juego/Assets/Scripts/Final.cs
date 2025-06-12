using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; 

public class FinalEscena : MonoBehaviour
{
    public float tiempoAntesDeSalir = 5f;

    void Start()
    {
        Invoke("IrAlMenuPrincipal", tiempoAntesDeSalir);
    }

    void IrAlMenuPrincipal()
    {
        EstadoJugador.inputBloqueado = false;
        EstadoJugador.esInvencible = false;

        string savePath = Path.Combine(Application.persistentDataPath, "datosGuardado.json");
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        SceneManager.LoadScene(0);
    }
}
