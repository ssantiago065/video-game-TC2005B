using UnityEngine;
using System.Collections;

public class controladorEscena : MonoBehaviour
{
    public GameObject grupoDrones2;
    public Animator animadorPantalla; 
    private bool etapa2Iniciada = false;

    public void ActivarEtapa2()
    {
        if (etapa2Iniciada) return;
        etapa2Iniciada = true;
        StartCoroutine(blackOut());
    }

    IEnumerator blackOut()
    {
        animadorPantalla.SetTrigger("blackOut");

        yield return new WaitForSeconds(1.5f);

        grupoDrones2.SetActive(true);
    }
}