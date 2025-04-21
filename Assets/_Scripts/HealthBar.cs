using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image healthFill;

    public void SetMaxHealth(int maxAmount)
    {
        healthSlider.maxValue = maxAmount;
        healthSlider.value = maxAmount;

        healthFill.color = healthGradient.Evaluate(1f);
    }

    public void SetCurrentHealth(int amount)
    {
        healthSlider.value = amount;

        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
}