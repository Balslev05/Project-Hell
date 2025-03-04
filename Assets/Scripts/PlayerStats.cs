using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxArmor = 100;
    [SerializeField] private int currentArmor;

    public HealthBar healthBar;
    public PlotArmorBar armorBar;

    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
    }
}
