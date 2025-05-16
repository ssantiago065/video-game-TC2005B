using UnityEngine;

public class ataqueSolaris : MonoBehaviour
{
    public GameObject puntoAtaque;
    public float radio = 0.5f;
    public LayerMask Enemigos;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Golpe");
            Attack();
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