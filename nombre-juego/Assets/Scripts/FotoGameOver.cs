using UnityEngine;
using UnityEngine.UI;

public class GameOverAlpha : MonoBehaviour
{
    public Image fondo;
    public float velocidad = 1f;
    private bool activar = false;

    void Start()
    {
        Color color = fondo.color;
        color.a = 0f;
        fondo.color = color;
    }

    void Update()
    {
        if (activar && fondo.color.a < 1f)
        {
            Color color = fondo.color;
            color.a += Time.unscaledDeltaTime * velocidad;
            fondo.color = color;
        }
    }

    public void Mostrar()
    {
        activar = true;
    }
}
