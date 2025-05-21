using UnityEngine;
using System.Collections;
using System.IO;

public class controlGuardado : MonoBehaviour
{
    private string saveLocation;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "datosGuardado.json");
        StartCoroutine(CargarJuegoLuegoDeUnFrame());
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

        datosGuardado datos = new datosGuardado
        {
            posicionJugador = hijoActivo.position,
            personajeActivo = cambioPersonajeScript != null ? cambioPersonajeScript.IndicePersonajeActivo : 0
        };

        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(saveLocation, json);

        Debug.Log("Juego guardado en: " + saveLocation);
        Debug.Log("Posición guardada: " + datos.posicionJugador + " Personaje activo: " + datos.personajeActivo);
    }

    IEnumerator CargarJuegoLuegoDeUnFrame()
    {
        yield return null; // Espera un frame para que otros scripts hayan inicializado

        if (!File.Exists(saveLocation))
        {
            Debug.Log("Archivo de guardado no existe, guardando estado inicial.");
            guardarJuego();
            yield break;
        }

        string json = File.ReadAllText(saveLocation);
        datosGuardado datos = JsonUtility.FromJson<datosGuardado>(json);

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

        // Esperar un frame más para que se active el personaje correcto
        yield return null;

        // Mover al hijo activo
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
            Debug.Log("Posición cargada: " + datos.posicionJugador);
        }
        else
        {
            Debug.LogError("No se encontró hijo activo después de cambiar personaje.");
        }
    }
}
