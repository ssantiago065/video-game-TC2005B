using UnityEngine;

public class valkBala : MonoBehaviour
{
    private GameObject jugador;
    private Rigidbody2D rb;
    public float velocidadBala;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("jugador");

        if (jugador == null)
        {
            Debug.LogError("Jugador no encontrado. Asegúrate de que tiene el tag 'jugador'.");
            return;
        }

        // Aseguramos que solo usamos el plano X-Y
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;
        rb.linearVelocity = direccion * velocidadBala;

        float rot = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0 , 0, rot);
    }
}
