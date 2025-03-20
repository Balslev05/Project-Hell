using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    Collider2D SpawnArea;

    [Header("Stats")]
    public int Health;

    public Vector2 FindRadnomPointInCollider()
    {
        if (SpawnArea)
        {
            float RandomX = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
            float RandomY = Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y);

            Vector2 SpawnPoint = new Vector2 (RandomX,RandomY);
            return SpawnPoint;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
