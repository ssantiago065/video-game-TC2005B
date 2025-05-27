using UnityEngine;
using System.Collections;

public class logicaDron : MonoBehaviour
{
    private float tiempoEspera = 19f;
    private float distancia = 50f;
    private float duracionMovimiento = 10f;

    // --- Ataque ---
    public Transform puntoDisparo;
    public GameObject plantillaBala;
    private float cooldownAtaque = 5f;
    private float tiempoSiguienteAtaque = 0f;

    // --- Control ---
    private Vector3 posicionInicial;
    private bool enMovimiento = false;
    public bool viendoDerecha = true;

    // --- Detención temporal ---
    private bool detenido = false;
    private float tiempoDetenido = 0f;

    void Start()
    {
        posicionInicial = transform.position;
        StartCoroutine(CicloMovimiento());
    }

    void Update()
    {
        if (MenuPausa.JuegoPausado) return;

        if (detenido)
        {
            tiempoDetenido -= Time.deltaTime;
            if (tiempoDetenido <= 0)
            {
                detenido = false;
                Debug.Log("Dron reanudó movimiento y ataque.");
            }
            else
            {
                // Mientras está detenido, no hace nada
                return;
            }
        }

        if (!enMovimiento && Time.time >= tiempoSiguienteAtaque)
        {
            Attack();
            tiempoSiguienteAtaque = Time.time + cooldownAtaque;
        }
    }

    IEnumerator CicloMovimiento()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEspera);

            if (!enMovimiento && !detenido)  // No se mueve si está detenido
            {
                enMovimiento = true;

                Vector3 direccion = viendoDerecha ? Vector3.right : Vector3.left;
                Vector3 destino = posicionInicial + direccion * distancia;

                // Mover hacia adelante
                yield return StartCoroutine(MoverA(destino));

                yield return new WaitForSeconds(1f);

                // Mover de regreso
                yield return StartCoroutine(MoverA(posicionInicial));

                enMovimiento = false;
            }
        }
    }

    IEnumerator MoverA(Vector3 destino)
    {
        Vector3 inicio = transform.position;
        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            if (detenido)
            {
                // Espera aquí mientras está detenido
                yield return null;
                continue;
            }

            transform.position = Vector3.Lerp(inicio, destino, tiempo / duracionMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = destino;
    }

    void Attack()
    {
        if (detenido) return;  // No ataca si está detenido

        int cantidadBalas = 5;
        float anguloInicial = -20f;
        float separacionAngulo = 10f;

        for (int i = 0; i < cantidadBalas; i++)
        {
            float angulo = anguloInicial + i * separacionAngulo;
            float rad = angulo * Mathf.Deg2Rad;

            Vector2 direccion = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            if (!viendoDerecha)
            {
                direccion.x = -direccion.x;
            }

            GameObject bala = Instantiate(plantillaBala, puntoDisparo.position, Quaternion.identity);

            bolaPlasma scriptBala = bala.GetComponent<bolaPlasma>();
            if (scriptBala != null)
            {
                scriptBala.direccion = direccion;
            }
        }
    }

    // Método público para detener al dron temporalmente
    public void DetenerTemporalmente(float segundos)
    {
        detenido = true;
        tiempoDetenido = segundos;
        Debug.Log("Dron detenido por " + segundos + " segundos.");
    }
}
