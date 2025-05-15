using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public GameObject attackPoint;
    public float radius = 0.5f;
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
        Collider2D[] enemigosHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, Enemigos);

        foreach (Collider2D Enemigo in enemigosHit)
        {
            Debug.Log("Enemigo golpeado: " + Enemigo.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}