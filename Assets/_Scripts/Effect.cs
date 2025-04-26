using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Apply(PlayerStats player);
    public abstract string Descreption();
}
