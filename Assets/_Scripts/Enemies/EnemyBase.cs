using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteRenderer bodySprite;
    [SerializeField] protected Animator animator;
    protected AIPath path;

    [Header("Stats")]
    [SerializeField] protected int threatValue;
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

    protected virtual void TakeDamage()
    {
        
    }

    protected virtual void Die()
    {
        
    }
}
