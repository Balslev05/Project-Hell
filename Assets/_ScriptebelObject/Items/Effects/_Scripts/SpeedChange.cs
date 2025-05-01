using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/SpeedChange", order = 1)]
public class SpeedChange : Effect
{
   public float SpeedBuff = 100;
    public bool randomize = false;
    [Header("Randomize")]
    public float min = 1;
    public float max = 1;
    public float RandomSpeed;

    public override void Apply(PlayerStats player)
    {
        if (randomize)
        {
            SpeedBuff = RandomSpeed;
        }
        player.BaseSpeed = Mathf.CeilToInt(player.BaseSpeed * SpeedBuff);
        
    }

    public override string Descreption()
    {
        RandomSpeed = Random.Range(min,max);
        if (randomize)
        {
            return "Speed Buff " + RandomSpeed.ToString("F1");;
        }
        else
        {
            return "Speed Buff " + SpeedBuff;
        }
    }
}
