using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject entity;
    private Health health;
    private float maxHealth;
    private float lerpSpeed = 0.05f;
    public Slider healthSlider;
    public Slider easeHealthSlider;

    void Start()
    {
        health = entity.GetComponent<Health>();
        maxHealth = health.MaxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        easeHealthSlider.maxValue = maxHealth;
        easeHealthSlider.value = maxHealth;
    }

    void Update()
    {
        if (healthSlider.value != health.CurrentHealth)
        {
            healthSlider.value = health.CurrentHealth;
        }

        if (easeHealthSlider.value != health.CurrentHealth)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health.CurrentHealth, lerpSpeed);
        }

        if (health.MaxHealth != healthSlider.maxValue)
        {
            healthSlider.maxValue = health.MaxHealth;
            easeHealthSlider.maxValue = health.MaxHealth;
        }
    }
}
