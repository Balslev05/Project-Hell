using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "WaveSystem/Wave", order = 1)]
public class Wave : ScriptableObject
{
    [Header("EnemiesAllowed")]
    public bool enemyNormalDuckAllowed;
    public bool enemyBuffDuckAllowed;
    public bool enemyPoliceDuckAllowed;
    public bool enemyMilitaryDuckAllowed;
    public bool enemyCoolGooseAllowed;
    
    public int enemyNormalDuckSpawnChance;
    public int enemyBuffDuckSpawnChance;
    public int enemyPoliceDuckSpawnChance;
    public int enemyMilitaryDuckSpawnChance;
    public int enemyCoolGooseSpawnChance;

    [Header("Variables")]
    public int totalThreatScore;
    public int allowedThreatLevel;
    public int timeBetweenSpawn;
    public int maxSpawnAmount;
    public int minSpawnAmount;
    public int waveCurrencyValue;
}
