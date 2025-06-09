using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalEscena : MonoBehaviour
{
    public float tiempoAntesDeSalir = 5f;

    void Start()
    {
        Invoke("IrAlMenuPrincipal", tiempoAntesDeSalir);
    }

    void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}