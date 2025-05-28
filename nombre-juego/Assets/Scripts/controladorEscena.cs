using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class controladorEscena : MonoBehaviour
{
    public GameObject grupoDrones2;
    public GameObject grupoDrones3;
    public Image pantallaFade;
    public float duracionFade = 1.5f;
    private bool etapa2Iniciada = false;
    private bool etapa3Iniciada = false;

    public void ActivarEtapa2()
    {
        if (etapa2Iniciada) return;
        etapa2Iniciada = true;
        StartCoroutine(FadeEntreFases());
        grupoDrones2.SetActive(true);
    }
    public void ActivarEtapa3()
    {
        if (etapa3Iniciada) return;
        etapa3Iniciada = true;
        StartCoroutine(FadeEntreFases());
        grupoDrones3.SetActive(true);
    }
    IEnumerator FadeEntreFases()
    {
        yield return StartCoroutine(Fade(0f, 1f)); // Fade to black
        yield return new WaitForSeconds(0.3f); // tiempo opcional para efecto dramático
        yield return StartCoroutine(Fade(1f, 0f)); // Fade out (mostrar escena)
    }

    public IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / duracionFade);
            Color color = pantallaFade.color;
            color.a = alpha;
            pantallaFade.color = color;
            yield return null;
        }

        // asegurar que llegue al valor final exacto
        Color finalColor = pantallaFade.color;
        finalColor.a = to;
        pantallaFade.color = finalColor;
    }
}