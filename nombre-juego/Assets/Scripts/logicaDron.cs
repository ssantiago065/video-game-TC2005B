using UnityEngine;
using System.Collections;

public class logicaDron : MonoBehaviour
{
    private float tiempoEspera = 19f;
    private float distancia = 40f;
    private float duracionMovimiento = 10f;

    public Transform puntoDisparo;
    public GameObject plantillaBala;
    private float cooldownAtaque = 5f;
    private float tiempoSiguienteAtaque = 0f;

    private Vector2 posicionInicial;
    private bool enMovimiento = false;
    public bool viendoDerecha = true;

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
            }
            else
            {
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

            if (!enMovimiento && !detenido)
            {
                enMovimiento = true;

                Vector2 direccion = viendoDerecha ? Vector2.right : Vector2.left;
                Vector2 destino = posicionInicial + direccion * distancia;

                yield return StartCoroutine(MoverA(destino));

                yield return new WaitForSeconds(1f);

                yield return StartCoroutine(MoverA(posicionInicial));

                enMovimiento = false;
            }
        }
    }

    IEnumerator MoverA(Vector2 destino)
    {
        Vector2 inicio = transform.position;
        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            if (detenido)
            {
                yield return null;
                continue;
            }

            transform.position = Vector2.Lerp(inicio, destino, tiempo / duracionMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = destino;
    }

    void Attack()
    {
        if (detenido) return;

        int cantidadBalas = 3;
        float anguloInicial = -10f;           
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

    public void DetenerTemporalmente(float segundos)
    {
        if (enMovimiento)
        {
            detenido = true;
            tiempoDetenido = segundos;
        }
    }

    public void Reiniciar()
    {
        transform.position = posicionInicial;
        detenido = false;
        tiempoDetenido = 0f;
    }
    public void Detener()
    {
        detenido = true;
    }
}
