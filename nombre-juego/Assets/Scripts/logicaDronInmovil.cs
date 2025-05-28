using UnityEngine;

public class logicaDronInmovil : MonoBehaviour
{
    // --- Ataque ---
    public Transform puntoDisparo;
    public GameObject plantillaBala;
    private float cooldownAtaque = 5f;
    private float tiempoSiguienteAtaque = 0f;

    void Update()
    {
        if (MenuPausa.JuegoPausado) return;

        if (Time.time >= tiempoSiguienteAtaque)
        {
            DronArribaAttack();
            tiempoSiguienteAtaque = Time.time + cooldownAtaque;
        }
    }

    void DronArribaAttack()
    {
        int cantidadBalas = 3;
        float anguloCentro = -90f;               // Centro hacia abajo
        float separacionAngulo = 10f;            // Ángulo entre cada bala

        float anguloInicial = anguloCentro - separacionAngulo; // = -100°
        
        for (int i = 0; i < cantidadBalas; i++)
        {
            float angulo = anguloInicial + i * separacionAngulo;
            float rad = angulo * Mathf.Deg2Rad;

            Vector2 direccion = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            GameObject bala = Instantiate(plantillaBala, puntoDisparo.position, Quaternion.identity);

            bolaPlasma scriptBala = bala.GetComponent<bolaPlasma>();
            if (scriptBala != null)
            {
                scriptBala.direccion = direccion;
            }
        }
    }
}
