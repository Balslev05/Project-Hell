using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    protected GameObject player;
    public SpriteRenderer bodySprite;
    public Animator animator;
    public Collider2D collider;
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
    [SerializeField] protected float attackDuration;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackRange;
    //[SerializeField] public int DeathVersions;

    protected float distanceToPlayer;
    public bool isDead;
    protected bool isMoving;
    protected bool inAttackRange;
    protected bool canAttack;
    protected bool isAttacking = false;

    protected virtual void Start()
    {
        path = GetComponent<AIPath>();
        collider = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
        playerStats = player.GetComponent<PlayerStats>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        Move();

        if (isDead) { return; }
    }

    protected virtual void Move()
    {
        path.maxSpeed = moveSpeed;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > attackRange && !playerAbilities.isGhosting && !isDead) {
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
