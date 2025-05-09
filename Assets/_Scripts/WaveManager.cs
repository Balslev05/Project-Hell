using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject enemyNormalDuckPrefab;
    public GameObject enemyBuffDuckPrefab;
    public GameObject enemyPoliceDuckPrefab;
    public GameObject enemyMilitaryDuckPrefab;
    public GameObject enemyCoolGoosePrefab;

    public GameObject SpawnMarkerPrefab;

    [Header("Components")]
    [SerializeField] private TMP_Text WaveNumberText;
    [SerializeField] private TMP_Text ThreatLevelText;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private ShopCanvas shopCanvas;
    [SerializeField] private List<Wave> waves = new List<Wave>();
    [SerializeField] private List<GameObject> PossibleEnemies = new List<GameObject>();
    [SerializeField] public List<GameObject> LiveEnemies = new List<GameObject>();
    [SerializeField] private Collider2D spawnArea;
    private int currentWave = 0;
    private int localTotalThreatScore;
    private int currentThreatLevel;
    private bool waveRunning = false;

    void Start()
    {
        OnClickStartWave();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnClickStartWave();
        }
    }

    public void OnClickStartWave()
    {
        if (!waveRunning) { StartCoroutine(PlayWave()); }
    }

    private IEnumerator PlayWave()
    {
        waveRunning = true;
        localTotalThreatScore = waves[currentWave].totalThreatScore;
        UpdateText();
        SetupPossibleEnemies();

        while (localTotalThreatScore > 0)
        {
            if (GetCurrentThreatLevel() < waves[currentWave].allowedThreatLevel)
            {
                int spawnAmount = Random.Range(waves[currentWave].minSpawnAmount, waves[currentWave].maxSpawnAmount + 1);

                for (int i = 0; i < spawnAmount; i++)
                {
                    StartCoroutine(SpawnEnemy(PossibleEnemies[Random.Range(0, PossibleEnemies.Count)]));
                    yield return new WaitForSeconds(0.25f);
                }
            }
            yield return new WaitForSeconds(waves[currentWave].timeBetweenSpawn);
        }

        while (LiveEnemies.Count > 0) { yield return new WaitForSeconds(5); }
        EndWave();
    }

    private void EndWave()
    {
        currencyManager.GetMoney(waves[currentWave].waveCurrencyValue);
        if (currentWave == waves.Count) { SceneManager.LoadScene("Win Screen"); }
        else
        {
            waveRunning = false;
            currentWave++;
            StartCoroutine(shopCanvas.ShowShop());
        }
    }

    private IEnumerator SpawnEnemy(GameObject EnemyToSpawn)
    {
        Vector2 SpawnPoint = FindRadnomPointInCollider();
        GameObject spawnMarker = Instantiate(SpawnMarkerPrefab, SpawnPoint, Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(spawnMarker);
        GameObject spawnedEnemy = Instantiate(EnemyToSpawn, SpawnPoint, Quaternion.identity);

        LiveEnemies.Add(spawnedEnemy);
        localTotalThreatScore -= spawnedEnemy.GetComponent<EnemyBase>().threatValue;
        UpdateText();
    }

    private void SetupPossibleEnemies()
    {
        if (PossibleEnemies.Count != 0) { PossibleEnemies.Clear(); }

        if (waves[currentWave].enemyNormalDuckAllowed) {
            for (int i = 0; i < waves[currentWave].enemyNormalDuckSpawnChance; i++) {
                PossibleEnemies.Add(enemyNormalDuckPrefab);
            }
        }
        if (waves[currentWave].enemyBuffDuckAllowed) {
            for (int i = 0; i < waves[currentWave].enemyBuffDuckSpawnChance; i++) {
                PossibleEnemies.Add(enemyBuffDuckPrefab);
            }
        }
        if (waves[currentWave].enemyPoliceDuckAllowed) {
            for (int i = 0; i < waves[currentWave].enemyPoliceDuckSpawnChance; i++) {
                PossibleEnemies.Add(enemyPoliceDuckPrefab);
            }
        }
        if (waves[currentWave].enemyMilitaryDuckAllowed) {
            for (int i = 0; i < waves[currentWave].enemyMilitaryDuckSpawnChance; i++) {
                PossibleEnemies.Add(enemyMilitaryDuckPrefab);
            }
        }
        if (waves[currentWave].enemyCoolGooseAllowed) {
            for (int i = 0; i < waves[currentWave].enemyCoolGooseSpawnChance; i++) {
                PossibleEnemies.Add(enemyCoolGoosePrefab);
            }
        }
    }

    private int GetCurrentThreatLevel()
    {
        currentThreatLevel = 0;

        foreach (GameObject enemy in LiveEnemies)
        {
            if (enemy == null) { LiveEnemies.Remove(enemy); }
            else
            {
                currentThreatLevel += enemy.GetComponent<EnemyBase>().threatValue;
            }
        }
        return currentThreatLevel;
    }

    private Vector2 FindRadnomPointInCollider()
    {
        if (spawnArea)
        {
            float RandomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float RandomY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);

            Vector2 SpawnPoint = new Vector2(RandomX, RandomY);
            return SpawnPoint;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private void UpdateText()
    {
        WaveNumberText.text = $"Wave: {(currentWave + 1)} / {waves.Count}";
        ThreatLevelText.text = $"Threat: {localTotalThreatScore} / {waves[currentWave].totalThreatScore}";
    }
}