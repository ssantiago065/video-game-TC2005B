using UnityEngine;

using UnityEngine.InputSystem;

public class ataqueAeterius : MonoBehaviour
{
    public Transform puntoDisparo;
    public GameObject plantillaBala;
    public float attackCooldown = 1f; // Tiempo entre ataques en segundos
    private float nextAttackTime = 0f;

    public void Update()
    {
        if (MenuPausa.JuegoPausado) return;

        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Debug.Log("Disparo");
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        GameObject bala = Instantiate(plantillaBala, puntoDisparo.position, Quaternion.identity);
        Bullet scriptBala = bala.GetComponent<Bullet>();

        // Usa la escala local del personaje para determinar la dirección
        float dirX = transform.localScale.x > 0 ? 1f : -1f;
        scriptBala.direction = new Vector2(dirX, 0f);
    }
}
