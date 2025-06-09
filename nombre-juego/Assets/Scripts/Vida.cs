using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        if (EstadoJugador.esInvencible) return;

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
