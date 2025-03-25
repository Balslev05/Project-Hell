using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Components")]
    public HealthBar healthBar;
    public PlotArmorBar armorBar;

    [Header("Stats")]
    public int maxHealth = 100;
    public float currentHealth;
    public int maxArmor = 100;
    public float currentArmor;


    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
        healthBar.SetMaxHealth(maxHealth);
        armorBar.SetMaxPlotArmor(maxArmor);
    }

    void Update()
    {
        // Debugs
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(50);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            HealHealth(5);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentHealth = maxHealth;
            currentArmor = maxArmor;
            healthBar.SetCurrentHealth(Mathf.FloorToInt(currentHealth));
            armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));
        }
    }

    public void TakeDamage(float amount)
    {
        float damageReduction = currentArmor / maxArmor;
        float reducedDamage = amount * (1 - damageReduction);
        currentHealth -= reducedDamage;

        if (currentArmor > 0)
        {
            currentArmor -= 1;
        }

        healthBar.SetCurrentHealth(Mathf.FloorToInt(currentHealth));
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("I'm dead D:");
    }

    public void HealHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        healthBar.SetCurrentHealth(Mathf.FloorToInt(currentHealth));
    }

    public void LoseArmor(float amount)
    {
        currentArmor -= amount;
        if (currentArmor < 0) { currentArmor = 0; }
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));
    }

    public void GainArmor(float amount)
    {
        currentArmor += amount;
        if (currentArmor > maxArmor) { currentArmor = maxArmor; }
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));
    }
}