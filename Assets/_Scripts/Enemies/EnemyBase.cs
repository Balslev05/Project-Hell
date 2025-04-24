using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    protected GameObject player;
    [SerializeField] protected SpriteRenderer bodySprite;
    [SerializeField] protected Animator animator;
    protected AIPath path;
    protected PlayerStats playerStats;
    protected PlayerAbilities playerAbilities;

    [Header("Stats")]
    [SerializeField] public int threatValue;
    [SerializeField] public int currencyValue;
    [SerializeField] protected int maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] public float damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;

    protected float distanceToPlayer;
    protected bool isMoving;
    protected bool inAttackRange;
    protected bool canAttack;
    protected bool isAttacking = false;

    protected virtual void Start()
    {
        path = GetComponent<AIPath>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
        playerStats = player.GetComponent<PlayerStats>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        path.maxSpeed = moveSpeed;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > attackRange && !playerAbilities.isGhosting) {
            isMoving = true; animator.SetBool("IsMoving", true);
            path.destination = player.transform.position;
            FlipSprite();
        }
        else {
            isMoving = false; animator.SetBool("IsMoving", false);
            path.destination = transform.position;
        }
    }

    protected void FlipSprite()
    {
        if (path.desiredVelocity.x >= 0.01f) { bodySprite.flipX = true; }
        else if (path.desiredVelocity.x <= -0.01f) { bodySprite.flipX = false; }
    }
}
