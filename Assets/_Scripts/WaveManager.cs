using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject enemyNormalDuckPrefab;
    public GameObject enemyBuffDuckPrefab;
    public GameObject enemyPoliceDuckPrefab;
    public GameObject enemyMilitaryDuckPrefab;
    
    public GameObject SpawnMarkerPrefab;

    [Header("Components")]
    [SerializeField] private List<Wave> waves = new List<Wave>();
    private int currentWave;
    [SerializeField] private Collider2D spawnArea;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        Vector2 SpawnPoint = FindRadnomPointInCollider();

        GameObject spawnMarker = Instantiate(SpawnMarkerPrefab, SpawnPoint, Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(spawnMarker);
        Instantiate(enemyNormalDuckPrefab, SpawnPoint, Quaternion.identity);
    }

    private Vector2 FindRadnomPointInCollider()
    {
        if (spawnArea) {
            float RandomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float RandomY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);

            Vector2 SpawnPoint = new Vector2(RandomX, RandomY);
            return SpawnPoint;
        }
        else {
            return Vector2.zero;
        }
    }
}
