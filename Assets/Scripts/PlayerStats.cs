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
        armorBar.SetMaxPlotArmor(maxArmor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HealHealth(5);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetCurrentHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("I'm dead D:");
    }

    void HealHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        healthBar.SetCurrentHealth(currentHealth);
    }
}
