using UnityEngine;
[CreateAssetMenu(fileName = "Effect", menuName = "Effects/AmmoChange", order = 1)]

public class AmmoChange : Effect
{
    public float AmmoAmount = 100;
    public bool randomize = false;
    [Header("Randomize")]
    public float min = 1;
    public float max = 1;
    public float RandomAmmo;

    public override void Apply(PlayerStats player)
    {
       player.gunManager.Maxammo += Mathf.CeilToInt(AmmoAmount);
    }

    public override string Descreption()
    {
        RandomAmmo = Random.Range(min,max);
        if (randomize)
        {
            return "Max Ammo Increase by" + RandomAmmo.ToString("F1");;
        }
        else
        {
            return "Max Ammo Increase by" + AmmoAmount;
        }
    }
}
