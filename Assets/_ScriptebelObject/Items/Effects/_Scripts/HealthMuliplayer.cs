using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/HealthMuliplayer", order = 1)]
public class HealthMuliplayer : Effect
{
    
    public float healthMuliplayer;
    public bool randomize = false;
    [Header("Randomize")]
    public float min;
    public float max;
    private float RandomizedHealth;
    
    public override void Apply(PlayerStats player)
    {

        if(!randomize)
        {
            Debug.Log("healthMuliplayer = " + healthMuliplayer);
            player.maxHealth = Mathf.FloorToInt(player.maxHealth * healthMuliplayer);
            player.currentHealth = Mathf.FloorToInt(player.currentHealth * healthMuliplayer);

        } else
        {
            Debug.Log("healthMuliplayer = " + RandomizedHealth);
            player.maxHealth = Mathf.FloorToInt(player.maxHealth * RandomizedHealth);
            player.currentHealth = Mathf.FloorToInt(player.currentHealth * RandomizedHealth);
        }
    }

    public override string Descreption()
    {
        RandomizedHealth = Random.Range(min,max);
        if (randomize)
        {
            return "Health Muliplayer " + this.RandomizedHealth.ToString("F2");

        }
        else
        {
            return "Health Muliplayer " + healthMuliplayer;
        }
    }
}
