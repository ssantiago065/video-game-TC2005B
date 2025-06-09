using UnityEngine;


public class logicaDronFinal : MonoBehaviour
{
    public Transform puntoDisparo;
    public GameObject plantillaBala;
    private float cooldownAtaque = 5f;
    private float tiempoSiguienteAtaque = 0f;
    public bool viendoDerecha = true;

    void Update()
    {
        if (MenuPausa.JuegoPausado) return;

        if (Time.time >= tiempoSiguienteAtaque)
        {
            dronFinalAttack();
            tiempoSiguienteAtaque = Time.time + cooldownAtaque;
        }
    }

    void dronFinalAttack()
    {
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
}
