using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteRenderer bodySprite;
    [SerializeField] protected Animator animator;
    protected AIPath path;
    protected Collider2D SpawnArea;
    

    [Header("Stats")]
    [SerializeField] protected int maxHealth;
    protected float currentHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    protected float distanceToTarget;

    protected bool inAttackRange;

    protected virtual void Start()
    {
        path = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
    }

    protected virtual void Move()
    {
        path.maxSpeed = moveSpeed;

        distanceToTarget = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToTarget > attackRange) {
            path.destination = player.transform.position;
            inAttackRange = false;
            FlipSprite();
        }
        else {
            path.destination = transform.position;
            inAttackRange = true;
        }
    }

    protected void FlipSprite()
    {
        if (path.desiredVelocity.x >= 0.01f) { bodySprite.flipX = true; }
        else if (path.desiredVelocity.x <= -0.01f) { bodySprite.flipX = false; }
    }

    protected virtual void Attack()
    {
        if (inAttackRange)
        {
            Debug.Log("Attacking");
        }
    }

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
