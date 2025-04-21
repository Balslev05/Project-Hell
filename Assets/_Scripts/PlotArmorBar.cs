using UnityEngine;
using UnityEngine.UI;

public class PlotArmorBar : MonoBehaviour
{
    public Slider armorSlider;
    public Gradient armorGradient;
    public Image armorFill;

    public void SetMaxPlotArmor(int maxAmount)
    {
        armorSlider.maxValue = maxAmount;
        armorSlider.value = maxAmount;

        armorFill.color = armorGradient.Evaluate(1f);
    }

    public void SetCurrentPlotArmor(int amount)
    {
        armorSlider.value = amount;

        armorFill.color = armorGradient.Evaluate(armorSlider.normalizedValue);
    }
}