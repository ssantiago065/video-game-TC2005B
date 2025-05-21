using UnityEngine;

public class ataqueSolaris : MonoBehaviour
{
    public GameObject puntoAtaque;
    public float radio = 0.5f;
    public LayerMask Enemigos;
    public float cooldownAtaque = 1f; 
    private float tiempoSiguienteAtaque = 0f;

    public void Update()
    {
        if (MenuPausa.JuegoPausado) return;

        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoSiguienteAtaque)
        {
            Debug.Log("Golpe");
            Attack();
            tiempoSiguienteAtaque = Time.time + cooldownAtaque;
        }
    }

    public void Attack()
    {
        Collider2D[] enemigosHit = Physics2D.OverlapCircleAll(puntoAtaque.transform.position, radio, Enemigos);

        foreach (Collider2D enemigo in enemigosHit)
        {
            Debug.Log("Enemigo golpeado: " + enemigo.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.transform.position, radio);
    }
}