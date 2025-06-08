using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public CambioPersonaje cambioPersonaje; // referencia al script en ListaPersonajes
    public float tiempoEntreDisparos = 2f;

    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= tiempoEntreDisparos)
        {
            Disparar();
            temporizador = 0f;
        }
    }

    void Disparar()
    {
        if (cambioPersonaje == null) return;

        Transform objetivo = cambioPersonaje.ObtenerPersonajeActivo();
        if (objetivo == null) return;

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        Vector2 direccion = objetivo.position - puntoDisparo.position;
        bala.GetComponent<Bullet>().SetDireccion(direccion);
    }
}
