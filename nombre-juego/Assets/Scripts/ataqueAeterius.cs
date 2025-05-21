using UnityEngine;

using UnityEngine.InputSystem;

public class ataqueAeterius : MonoBehaviour
{
    public Transform puntoDisparo;
    public GameObject plantillaBala;
    public float cooldownAtaque = 1f;
    private float tiempoSiguienteAtaque = 0f;

    public void Update()
    {
        if (MenuPausa.JuegoPausado) return;

        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoSiguienteAtaque)
        {
            Debug.Log("Disparo");
            Attack();
            tiempoSiguienteAtaque = Time.time + cooldownAtaque;
        }
    }

    void Attack()
    {
        GameObject bala = Instantiate(plantillaBala, puntoDisparo.position, Quaternion.identity);
        Shuriken scriptBala = bala.GetComponent<Shuriken>();

        float dirX = transform.localScale.x > 0 ? 1f : -1f;
        scriptBala.direccion = new Vector2(dirX, 0f);
    }
}
