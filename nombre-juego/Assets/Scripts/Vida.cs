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

    void Die()
    {
        Destroy(gameObject);
    }

    // Getter público
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // Setter público
    public void SetCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        UpdateHealthBar();
    }
}
