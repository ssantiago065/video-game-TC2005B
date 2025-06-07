using UnityEngine;

public class valkDisparo : MonoBehaviour
{
    public GameObject bala;
    public Transform posBala;
    private float timer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            Disparo();
        }
    }

    void Disparo()
    {
        if (bala != null && posBala != null)
        {
            Instantiate(bala, posBala.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("bala o posBala no asignados en el inspector.");
        }
    }
}
