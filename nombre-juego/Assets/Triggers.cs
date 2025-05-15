using UnityEngine;
public class Triggers: MonoBehaviour
{
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Trigger iniciado con: " + collision.gameObject.name);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Trigger manteniendose con: " + collision.gameObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Trigger Finalizado con: " + collision.gameObject.name);
        }
    }
}