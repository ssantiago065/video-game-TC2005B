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
        StartCoroutine(CargarJuegoLuegoDeUnFrame());
    }

    public void guardarJuego()
    {
        GameObject padre = GameObject.FindGameObjectWithTag("jugador");
        if (padre == null)
        {
            return;
        }

        CambioPersonaje cambioPersonajeScript = padre.GetComponent<CambioPersonaje>();
        if (cambioPersonajeScript == null)
        {
            return;
        }

        Transform personajeActivo = cambioPersonajeScript.ObtenerPersonajeActivo();
        if (personajeActivo == null)
        {
            return;
        }

        Vida vidaScript = padre.GetComponent<Vida>();
        float vida = vidaScript != null ? vidaScript.GetCurrentHealth() : -1f;

        datosGuardado datos = new datosGuardado
        {
            posicionJugador = personajeActivo.position,
            personajeActivo = cambioPersonajeScript.IndicePersonajeActivo,
            indiceNivel = SceneManager.GetActiveScene().buildIndex,
            vidaActual = vida
        };

        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(saveLocation, json);

    }

    IEnumerator CargarJuegoLuegoDeUnFrame()
    {
        yield return null;

        if (!File.Exists(saveLocation))
        {
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
            yield break;
        }

        CambioPersonaje cambioPersonajeScript = padre.GetComponent<CambioPersonaje>();
        if (cambioPersonajeScript != null)
        {
            cambioPersonajeScript.CambiarPersonajePorIndice(datos.personajeActivo);
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

            Vida vidaScript = padre.GetComponent<Vida>();
            if (vidaScript != null)
            {
                vidaScript.SetCurrentHealth(datos.vidaActual);
                vidaScript.SendMessage("UpdateHealthBar", SendMessageOptions.DontRequireReceiver);
            }

        }
    }
}
