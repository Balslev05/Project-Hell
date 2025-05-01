using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/SelfHarm", order = 1)]
public class SelfHarm : Effect
{
    public override void Apply(PlayerStats player)
    {
        player.selfHarm = true;
    }

    public override string Descreption()
    {
        return "Self Harm";
    }
}
