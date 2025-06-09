using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class transicion : MonoBehaviour
{
    public Image imagenFade;
    public float duracion = 1f;

    void Start()
    {
        SetAlpha(0f);
    }

    public void FadeIn() => StartCoroutine(Fade(1f));  
    public void FadeOut() => StartCoroutine(Fade(0f));

    IEnumerator Fade(float alphaFinal)
    {
        float alphaInicial = imagenFade.color.a;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duracion;
            float alphaActual = Mathf.Lerp(alphaInicial, alphaFinal, t);
            SetAlpha(alphaActual);
            yield return null;
        }

        SetAlpha(alphaFinal);
    }

    private void SetAlpha(float alpha)
    {
        Color c = imagenFade.color;
        c.a = alpha;
        imagenFade.color = c;
    }
}
