using UnityEngine;

public class ataqueSolaris : MonoBehaviour
{
    public GameObject puntoAtaque;
    public float radio = 0.5f;
    public LayerMask Enemigos;
    public float cooldownAtaque = 1f; 
    private float tiempoSiguienteAtaque = 0f;
    public LayerMask ParedDebil;

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
        // Atacar enemigos
        Collider2D[] zonasGolpeadas = Physics2D.OverlapCircleAll(puntoAtaque.transform.position, radio, Enemigos);

        foreach (Collider2D zona in zonasGolpeadas)
        {
            DamageZone damageZone = zona.GetComponent<DamageZone>();
            if (damageZone != null)
            {
                damageZone.RecibirGolpe();
                Debug.Log("Dron golpeado por ataque de Solaris");
            }
        }

        // Romper paredes débiles con el mismo puntoAtaque
        Collider2D[] paredes = Physics2D.OverlapCircleAll(puntoAtaque.transform.position, radio, ParedDebil);

        foreach (Collider2D pared in paredes)
        {
            if (pared.CompareTag("paredDebil"))
            {
                Debug.Log("Pared destruida antes del impacto");
                Destroy(pared.gameObject);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.transform.position, radio);
    }
}