using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteRenderer bodySprite;
    [SerializeField] protected Animator animator;
    protected AIPath path;
    protected PlayerAbilities playerAbilities;

    [Header("Stats")]
    [SerializeField] public int threatValue;
    [SerializeField] public int currencyValue;
    [SerializeField] protected int maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;

    protected bool inAttackRange;

    protected virtual void Start()
    {
        path = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();

        currentHealth = maxHealth;
    }

    protected virtual void Move()
    {
        path.maxSpeed = moveSpeed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > attackRange && !playerAbilities.isGhosting) {
            inAttackRange = false;
            path.destination = player.transform.position;
            FlipSprite();
        }
        else {
            inAttackRange = true;
            path.destination = transform.position;
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
            
        }
    }
}
