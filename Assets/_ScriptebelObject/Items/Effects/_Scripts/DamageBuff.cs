using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/DamageBuff", order = 1)]
public class DamageBuff : Effect
{
    public float damageBuff = 100;
    public bool randomize = false;
    [Header("Randomize")]
    public float min = 1;
    public float max = 1;
    public float randomDamage;

    public override void Apply(PlayerStats player)
    {
        foreach (Gun gun in player.gunManager.GunList)
        {
            gun.damage = Mathf.CeilToInt(gun.damage * damageBuff);
        }
    }

    public override string Descreption()
    {
        randomDamage = Random.Range(min,max);
        if (randomize)
        {
            return "Damage Buff " + randomDamage.ToString("F1");;
        }
        else
        {
            return "Damage Buff " + damageBuff;
        }
    }
}
