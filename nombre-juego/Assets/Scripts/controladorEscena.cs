using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class controladorEscena : MonoBehaviour
{
    public GameObject grupoDrones2;
    public GameObject grupoDrones3;
    public GameObject paredesFaseFinal;
    public Image pantallaFade;
    public float duracionFade = 1.5f;
    private bool etapa2Iniciada = false;
    private bool etapa3Iniciada = false;

    public void ActivarEtapa2()
    {
        if (etapa2Iniciada) return;
        etapa2Iniciada = true;
        StartCoroutine(FadeEntreFases(grupoDrones2, null));
    }

    public void ActivarEtapa3()
    {
        if (etapa3Iniciada) return;
        etapa3Iniciada = true;
        StartCoroutine(FadeEntreFases(grupoDrones3, paredesFaseFinal));
    }

    IEnumerator FadeEntreFases(GameObject grupoPrincipal, GameObject grupoExtra)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        if (grupoPrincipal != null)
            grupoPrincipal.SetActive(true);

        if (grupoExtra != null)
            grupoExtra.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(Fade(1f, 0f));
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

        Color finalColor = pantallaFade.color;
        finalColor.a = to;
        pantallaFade.color = finalColor;
    }
}