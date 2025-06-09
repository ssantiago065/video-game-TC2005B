using UnityEngine;

public class CambioPersonaje : MonoBehaviour
{
    private GameObject[] listaPersonajes;
    private int indice = 0;
    private bool isGrounded;
    float[] ajustesAltura = new float[] { 0f, 0.4f };
    public int IndicePersonajeActivo => indice;

    public void CambiarPersonajePorIndice(int nuevoIndice)
    {
        if (nuevoIndice < 0 || nuevoIndice >= listaPersonajes.Length) return;

        listaPersonajes[indice].SetActive(false);

        indice = nuevoIndice;

        listaPersonajes[indice].SetActive(true);

    }


    private void Start()
    {
        listaPersonajes = new GameObject[2];

        for (int i = 0; i < 2; i++)
            listaPersonajes[i] = transform.GetChild(i).gameObject;

        foreach (GameObject personaje in listaPersonajes)
            personaje.SetActive(false);

        if (listaPersonajes[0])
            listaPersonajes[0].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cambioPersonaje();
        }
    }

    public void cambioPersonaje()
    {
        Rigidbody2D cuerpoActual = listaPersonajes[indice].GetComponent<Rigidbody2D>();

        Vector3 posicionActual = listaPersonajes[indice].transform.position;
        Vector2 velocidadActual = cuerpoActual != null ? cuerpoActual.linearVelocity : Vector2.zero;

        listaPersonajes[indice].SetActive(false);

        if (listaPersonajes[indice].TryGetComponent(out saltoSolaris scriptSolarisSaliente))
        {
            if (scriptSolarisSaliente.isDashing)
            {
                scriptSolarisSaliente.isDashing = false;

                Rigidbody2D rb = listaPersonajes[indice].GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.gravityScale = 1f;

                TrailRenderer tr = listaPersonajes[indice].GetComponent<TrailRenderer>();
                if (tr != null)
                    tr.emitting = false;
            }
        }

        indice = (indice == 0) ? 1 : 0;

        Vector3 nuevaPos = posicionActual;
        nuevaPos.y += ajustesAltura[indice] - ajustesAltura[(indice == 0) ? 1 : 0];
        listaPersonajes[indice].transform.position = nuevaPos;

        listaPersonajes[indice].SetActive(true);

        Rigidbody2D nuevoCuerpo = listaPersonajes[indice].GetComponent<Rigidbody2D>();
        if (nuevoCuerpo != null)
            nuevoCuerpo.linearVelocity = velocidadActual;

        RaycastHit2D impacto = Physics2D.Raycast(listaPersonajes[indice].transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Suelo"));

        if (listaPersonajes[indice].TryGetComponent(out saltoSolaris scriptSolaris))
        {
            scriptSolaris.isGrounded = impacto.collider != null;
        }
        else if (listaPersonajes[indice].TryGetComponent(out saltoAeterius scriptAeterius))
        {
            scriptAeterius.isGrounded = impacto.collider != null;
        }
    }
    
    public Transform ObtenerPersonajeActivo()
    {
        return listaPersonajes[indice].transform;
    }
}