using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class controlGuardado : MonoBehaviour
{
    private string saveLocation;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "datosGuardado.json");
        //StartCoroutine(CargarJuegoLuegoDeUnFrame());
    }

    public void guardarJuego()
    {
        GameObject padre = GameObject.FindGameObjectWithTag("jugador");
        if (padre == null)
        {
            Debug.LogError("No se encontró objeto con tag 'jugador'");
            return;
        }

        Transform hijoActivo = null;
        foreach (Transform hijo in padre.transform)
        {
            if (hijo.gameObject.activeSelf)
            {
                hijoActivo = hijo;
                break;
            }
        }

        if (hijoActivo == null)
        {
            Debug.LogError("No se encontró hijo activo dentro del jugador");
            return;
        }

        CambioPersonaje cambioPersonajeScript = padre.GetComponent<CambioPersonaje>();
        Vida vidaScript = hijoActivo.GetComponent<Vida>();

        datosGuardado datos = new datosGuardado
        {
            posicionJugador = hijoActivo.position,
            personajeActivo = cambioPersonajeScript != null ? cambioPersonajeScript.IndicePersonajeActivo : 0,
            indiceNivel = SceneManager.GetActiveScene().buildIndex,
            vidaActual = vidaScript != null ? vidaScript.GetCurrentHealth() : 100f
        };

        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(saveLocation, json);

        Debug.Log($"Guardado: nivel {datos.indiceNivel}, personaje {datos.personajeActivo}, vida {datos.vidaActual}");
    }

    IEnumerator CargarJuegoLuegoDeUnFrame()
    {
        yield return null;

        if (!File.Exists(saveLocation))
        {
            Debug.Log("Archivo de guardado no existe, guardando estado inicial.");
            guardarJuego();
            yield break;
        }

        string json = File.ReadAllText(saveLocation);
        datosGuardado datos = JsonUtility.FromJson<datosGuardado>(json);

        if (SceneManager.GetActiveScene().buildIndex != datos.indiceNivel)
        {
            SceneManager.LoadScene(datos.indiceNivel);
            yield break;
        }

        GameObject padre = GameObject.FindGameObjectWithTag("jugador");
        if (padre == null)
        {
            Debug.LogError("No se encontró objeto con tag 'jugador' para cargar posición");
            yield break;
        }

        CambioPersonaje cambioPersonajeScript = padre.GetComponent<CambioPersonaje>();
        if (cambioPersonajeScript != null)
        {
            cambioPersonajeScript.CambiarPersonajePorIndice(datos.personajeActivo);
            Debug.Log("Personaje cambiado al índice: " + datos.personajeActivo);
        }

        yield return null;

        Transform hijoActivo = null;
        foreach (Transform hijo in padre.transform)
        {
            if (hijo.gameObject.activeSelf)
            {
                hijoActivo = hijo;
                break;
            }
        }

        if (hijoActivo != null)
        {
            hijoActivo.position = datos.posicionJugador;
            Vida vidaScript = hijoActivo.GetComponent<Vida>();
            if (vidaScript != null)
            {
                vidaScript.SetCurrentHealth(datos.vidaActual);
                Debug.Log("Vida cargada: " + datos.vidaActual);
            }

            Debug.Log("Posición cargada: " + datos.posicionJugador);
        }
        else
        {
            Debug.LogError("No se encontró hijo activo después de cambiar personaje.");
        }
    }
}
