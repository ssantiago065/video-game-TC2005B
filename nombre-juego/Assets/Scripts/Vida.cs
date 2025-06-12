using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthSlider;
    controladorAudio controladorAudio;

    void Awake()
    {
        controladorAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<controladorAudio>();
        if (currentHealth <= 0f)
        {
            currentHealth = maxHealth;
        }
    }

    void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        if (EstadoJugador.esInvencible) return;

        controladorAudio.PlaySFX(controladorAudio.dmgRecibido);
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;
    }

    public void Die()
    {
        controladorAudio.PlaySFX(controladorAudio.muerte);
        Time.timeScale = 0f;
        MenuPausa.EsGameOver = true;

        FotoGameOver pantallaGO = FindFirstObjectByType<FotoGameOver>();
        if (pantallaGO != null)
        {
            pantallaGO.Mostrar();
        }

        gameObject.SetActive(false); 
    }

    public float GetCurrentHealth() => currentHealth;

    public void SetCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        UpdateHealthBar();
    }
}
