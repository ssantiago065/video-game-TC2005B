using UnityEngine;

public class dañoDron : MonoBehaviour
{
    private logicaDron dron;

    void Start()
    {
        dron = GetComponentInParent<logicaDron>();
    }

    public void RecibirGolpe()
    {
        if (dron != null)
        {
            dron.DetenerTemporalmente(10f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("shuriken"))
        {
            RecibirGolpe();
            Destroy(other.gameObject);
        }
    }
}