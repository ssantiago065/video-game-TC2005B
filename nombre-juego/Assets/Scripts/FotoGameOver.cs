using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FotoGameOver : MonoBehaviour
{
    public Image imagenGameOver;
    public float duracionFade = 1.5f;
    public GameObject contenedorBotones;

    public void Mostrar()
    {
        if (imagenGameOver == null) return;

        gameObject.SetActive(true);
        imagenGameOver.gameObject.SetActive(true);
        if (contenedorBotones != null)
            contenedorBotones.SetActive(true);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        Color colorOriginal = imagenGameOver.color;
        colorOriginal.a = 0f;
        imagenGameOver.color = colorOriginal;

        while (t < duracionFade)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duracionFade);
            Color nuevoColor = imagenGameOver.color;
            nuevoColor.a = alpha;
            imagenGameOver.color = nuevoColor;
            yield return null;
        }

        Color finalColor = imagenGameOver.color;
        finalColor.a = 1f;
        imagenGameOver.color = finalColor;
    }
}
