using UnityEngine;

public class controladorAudio : MonoBehaviour
{
    [Header("------------ Audio Source ----------------")]
    [SerializeField] AudioSource fuenteMusica;
    [SerializeField] AudioSource fuenteSFX;

    [Header("------------ Audio Clip ----------------")]
    public AudioClip dobleSalto;
    public AudioClip Shuriken;
    public AudioClip Dash;
    public AudioClip Golpe;
    public AudioClip Cambio;
    public AudioClip musicaNivel;
    public AudioClip dmgRecibido;
    public AudioClip muerte;
    public AudioClip nivelFinalizado;
    public AudioClip paredRota;
    public AudioClip Salto;

    private void Start()
    {
        float volMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
        float volSFX = PlayerPrefs.GetFloat("VolumenSFX", 0.5f);

        fuenteMusica.volume = volMusica;
        fuenteSFX.volume = volSFX;

        fuenteMusica.clip = musicaNivel;
        fuenteMusica.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        fuenteSFX.PlayOneShot(clip);
    }

    public void CambiarVolumenMusica(float valor)
    {
        fuenteMusica.volume = valor;
        PlayerPrefs.SetFloat("VolumenMusica", valor);
    }

    public void CambiarVolumenSFX(float valor)
    {
        fuenteSFX.volume = valor;
        PlayerPrefs.SetFloat("VolumenSFX", valor);
    }
}
