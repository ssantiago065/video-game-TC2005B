using UnityEngine;

public class ataqueSolaris : MonoBehaviour
{
    public GameObject puntoAtaque;
    public float radio = 0.5f;
    public LayerMask Enemigos;
    public float cooldownAtaque = 1f; 
    private float tiempoSiguienteAtaque = 0f;
    public LayerMask ParedDebil;
    controladorAudio controladorAudio;
    private saltoSolaris movimientoS;

    private void Awake()
    {
        controladorAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<controladorAudio>();
        movimientoS = GetComponent<saltoSolaris>();
    }

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
        Collider2D[] zonasGolpeadas = Physics2D.OverlapCircleAll(puntoAtaque.transform.position, radio, Enemigos);
        
        bool golpeoEnemigo = false;
        foreach (Collider2D zona in zonasGolpeadas)
        {
            dañoDron damageZone = zona.GetComponent<dañoDron>();
            if (damageZone != null)
            {
                damageZone.RecibirGolpe();
                golpeoEnemigo = true;
                Debug.Log("Dron golpeado por ataque de Solaris");
            }
        }

        if (golpeoEnemigo && (movimientoS == null || !movimientoS.isDashing))
        {
            controladorAudio.PlaySFX(controladorAudio.Golpe);
        }

        Collider2D[] paredes = Physics2D.OverlapCircleAll(puntoAtaque.transform.position, radio, ParedDebil);
        foreach (Collider2D pared in paredes)
        {
            if (pared.CompareTag("paredDebil"))
            {
                Debug.Log("Pared destruida antes del impacto");
                controladorAudio.PlaySFX(controladorAudio.paredRota);
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