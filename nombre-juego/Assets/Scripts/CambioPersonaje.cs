using UnityEngine;

public class CambioPersonaje : MonoBehaviour
{
    private GameObject[] listaPersonajes;
    private int indice = 0;
    private bool isGrounded;
    float[] ajustesAltura = new float[] { 0f, 1f };
    public int IndicePersonajeActivo => indice;

    public void CambiarPersonajePorIndice(int nuevoIndice)
    {
        if (nuevoIndice < 0 || nuevoIndice >= listaPersonajes.Length) return;

        // Desactivar el personaje actual
        listaPersonajes[indice].SetActive(false);

        // Cambiar índice
        indice = nuevoIndice;

        // Activar el nuevo personaje
        listaPersonajes[indice].SetActive(true);

        // (Opcional) Ajustar posición si quieres mantener la posición previa aquí, 
        // pero el control de posición lo hacemos en controlGuardado ahora.
    }


    private void Start()
    {
        listaPersonajes = new GameObject[2];

        // Llenar el arreglo con nuestros modelos
        for (int i = 0; i < 2; i++)
            listaPersonajes[i] = transform.GetChild(i).gameObject;

        // Desactivar todos los personajes
        foreach (GameObject personaje in listaPersonajes)
            personaje.SetActive(false);

        // Activar el primer personaje
        if (listaPersonajes[0])
            listaPersonajes[0].SetActive(true);
    }

    private void Update()
    {
        // Verificar si se presiono la tecla Q
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

        // Resetear dash si el personaje actual es Solaris y está dasheando
        if (listaPersonajes[indice].TryGetComponent(out saltoSolaris scriptSolarisSaliente))
        {
            if (scriptSolarisSaliente.isDashing)
            {
                scriptSolarisSaliente.isDashing = false;

                // Restaurar la gravedad si estaba desactivada
                Rigidbody2D rb = listaPersonajes[indice].GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.gravityScale = 1f;

                // Apagar el trail si estaba emitiendo
                TrailRenderer tr = listaPersonajes[indice].GetComponent<TrailRenderer>();
                if (tr != null)
                    tr.emitting = false;
            }
        }

        indice = (indice == 0) ? 1 : 0;

        // Mover el nuevo personaje a la posicion anterior
        Vector3 nuevaPos = posicionActual;
        nuevaPos.y += ajustesAltura[indice] - ajustesAltura[(indice == 0) ? 1 : 0];
        listaPersonajes[indice].transform.position = nuevaPos;

        // Activar el nuevo personaje
        listaPersonajes[indice].SetActive(true);

        // Asignar la misma velocidad si tiene Rigidbody2D
        Rigidbody2D nuevoCuerpo = listaPersonajes[indice].GetComponent<Rigidbody2D>();
        if (nuevoCuerpo != null)
            nuevoCuerpo.linearVelocity = velocidadActual;
        
        RaycastHit2D impacto = Physics2D.Raycast(listaPersonajes[indice].transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Suelo"));

        // Asignar isGrounded segun el resultado
        if (listaPersonajes[indice].TryGetComponent(out saltoSolaris scriptSolaris))
        {
            scriptSolaris.isGrounded = impacto.collider != null;
        }
        else if (listaPersonajes[indice].TryGetComponent(out saltoAeterius scriptAeterius))
        {
            scriptAeterius.isGrounded = impacto.collider != null;
        }
    }
}