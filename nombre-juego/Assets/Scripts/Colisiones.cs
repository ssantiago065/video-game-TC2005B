using UnityEngine;
public class Colisiones : MonoBehaviour
{
    void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Colisión iniciada con: " + collision.gameObject.name);
        }
    }

    void onCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Colisión manteniendose con: " + collision.gameObject.name);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Colisión Finalizada con: " + collision.gameObject.name);
        }
    }
}
