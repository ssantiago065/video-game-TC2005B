using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalEscena : MonoBehaviour
{
    public float tiempoAntesDeSalir = 5f; // Tiempo en segundos antes de volver al menú

    void Start()
    {
        Invoke("IrAlMenuPrincipal", tiempoAntesDeSalir);
    }

    void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene(0); // Asume que el menú es la escena 0
    }
}