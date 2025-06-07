using UnityEngine;

public class controladorAudio : MonoBehaviour
{
    [Header("------------ Audio Source ----------------")]
    [SerializeField] AudioSource fuenteMusica;
    [SerializeField] AudioSource fuenteSFX;

    [Header("------------ Audio Clip ----------------")]
    public AudioClip dobleSalto;
    public AudioClip Shuriken;
    public AudioClip wallJump;
    public AudioClip Dash;
    public AudioClip Golpe;
    public AudioClip Cambio;
    public AudioClip musicaNivel;
    public AudioClip musicaJefe;
    public AudioClip dmgRecibido;
    public AudioClip muerte;
    public AudioClip nivelFinalizado;
    public AudioClip dmgEnemigos;
    public AudioClip paredRota;
    public AudioClip Salto;

    private void Start()
    {
        fuenteMusica.clip = musicaNivel;
        fuenteMusica.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        fuenteSFX.PlayOneShot(clip);
    }
}
