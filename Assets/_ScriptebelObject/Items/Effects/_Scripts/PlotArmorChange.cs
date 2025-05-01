using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/PlotArmorChange", order = 1)]
public class PlotArmorChange : Effect
{
   public float PlotMultiplayer;
    public bool randomize = false;
    [Header("Randomize")]
    public float min;
    public float max;
    private float RandomizedPlot;
    
    
    
    public override void Apply(PlayerStats player)
    {

        if(!randomize)
        {
            Debug.Log("plotMuliplayer = " + PlotMultiplayer);
            player.ArmorCostMultiplayer = player.ArmorCostMultiplayer * PlotMultiplayer;

        } else
        {
            Debug.Log("plotMuliplayer = " + RandomizedPlot);
            player.ArmorCostMultiplayer = player.ArmorCostMultiplayer * RandomizedPlot;
        }
    }

    public override string Descreption()
    {
         RandomizedPlot = Random.Range(min, max);
        if (randomize)
        {
            return "Plot Multiplier: X" + this.RandomizedPlot.ToString("F1");

        }
        else
        {
            return "Plot Multiplier: X" + PlotMultiplayer;
        }
    }
}
