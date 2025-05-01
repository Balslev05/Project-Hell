using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Components")]
    public HealthBar healthBar;
    public PlotArmorBar armorBar;

    [Header("Stats")]
    public int BaseDamage;
    public int BaseSpeed;
    public int maxHealth = 100;
    public float currentHealth;
    public int maxArmor = 100;
    public float currentArmor;
    public bool selfHarm = false;
    public float ArmorCostMultiplayer = 1f;

    [Header("Inventory")]
    List<Item> Items = new List<Item>();
    public GunManager gunManager;

    void Start()
    {
        gunManager = GetComponent<GunManager>();

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

        if (currentArmor > 0) { currentArmor--; }

        healthBar.SetCurrentHealth(Mathf.FloorToInt(currentHealth));
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));

        if (currentHealth <= 0) { Die(); }
    }

    void Die()
    {
        Debug.Log("I'm dead D:");
    }

    public void HealHealth(int amount)
    {
        currentHealth += amount;;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        healthBar.SetCurrentHealth(Mathf.FloorToInt(currentHealth));
    }

    public void LoseArmor(float amount)
    {
        currentArmor -= amount * ArmorCostMultiplayer;
        if (currentArmor < 0) { currentArmor = 0; }
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));
    }

    public void GainArmor(float amount)
    {
        currentArmor += amount;
        if (currentArmor > maxArmor) { currentArmor = maxArmor; }
        armorBar.SetCurrentPlotArmor(Mathf.FloorToInt(currentArmor));
    }


    public void Applyitem(Item item)
    {
         for (int i = 0; i < item.effects.Count; i++)
            {
                item.effects[i].Apply(this);
            }
    }
   /*  public void ApplyItems()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].effects.Count; j++)
            {
                Items[i].effects[j].Apply(this);
            }
        }
    } */
}