using UnityEngine;

public class EnemyMilitaryDuck : EnemyBase
{
    [Header("MilitaryDuckSpecific")]
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform shotPoint;
    [SerializeField] protected float attackRange;
    [SerializeField] private float attackCooldown;
    private void GunLookAtPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        gun.transform.up = playerPosition - new Vector2(transform.position.x, transform.position.y);
    }
}
