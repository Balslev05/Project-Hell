using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "WaveSystem/Wave", order = 1)]
public class Wave : ScriptableObject
{
    [Header("EnemiesAllowed")]
    public bool enemyNormalDuckAllowed;
    public bool enemyBuffDuckAllowed;
    public bool enemyPoliceDuckAllowed;
    public bool enemyMilitaryDuckAllowed;

    [Header("Components")]
    public int allowedThreatLevel;
    public int totalThreatScore;
}
