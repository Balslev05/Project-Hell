using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    private Collider2D SpawnArea;

    [Header("Stats")]
    [SerializeField] protected int maxHealth;
    protected float currentHealth;

    protected Vector2 FindRadnomPointInCollider()
    {
        if (SpawnArea) {
            float RandomX = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
            float RandomY = Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y);

            Vector2 SpawnPoint = new Vector2 (RandomX,RandomY);
            return SpawnPoint;
        }
        else {
            return Vector2.zero;
        }
    }
}
