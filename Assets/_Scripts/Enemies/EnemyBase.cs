using UnityEngine;
using Pathfinding;
using DG.Tweening.Core.Easing;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer bodySprite;
    public Animator animator;
    public Collider2D collider;
    protected AIPath path;

    protected GameObject player;
    protected PlayerStats playerStats;
    protected PlayerAbilities playerAbilities;

    protected Managers manager;
    protected WaveManager waveManager;

    [Header("Stats")]
    public int threatValue;
    public int currencyValue;
    [SerializeField] protected int maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float MoveToRange;
    public int damage;
    //[SerializeField] public int DeathVersions;

    protected float distanceToPlayer;
    public bool isDead;
    protected bool isMoving;
    protected bool inAttackRange;
    protected bool canAttack = true;
    protected bool isAttacking = false;

    protected virtual void Start()
    {
        path = GetComponent<AIPath>();
        collider = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
        playerStats = player.GetComponent<PlayerStats>();

        manager = GameObject.FindWithTag("Manager").GetComponent<Managers>();
        waveManager = manager.waveManager;

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
        if (!isDead && distanceToPlayer > MoveToRange && !isAttacking && !playerAbilities.isGhosting) {
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

    public virtual void Die()
    {
        isDead = true;
        waveManager.LiveEnemies.Remove(this.gameObject);
        collider.enabled = false;
    }
}
