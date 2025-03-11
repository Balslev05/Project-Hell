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
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HealHealth(5);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetCurrentHealth(Mathf.FloorToInt(currentHealth));
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
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));
        if (currentArmor < 0) { currentArmor = 0; }
    }
}
