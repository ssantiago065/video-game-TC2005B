using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class controladorNivel : MonoBehaviour
{
    public Image pantallaFade; 
    public float duracionFade = 1.5f;
    controladorAudio controladorAudio;

    private void Awake()
    {
        controladorAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<controladorAudio>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jugador"))
        {
            controladorAudio.PlaySFX(controladorAudio.nivelFinalizado);
            StartCoroutine(FinPartidaConFade());
        }
    }

    IEnumerator FinPartidaConFade()
    {

        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(2);
    }

    IEnumerator Fade(float from, float to)
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
