using UnityEngine;
using Pathfinding;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer bodySprite;
    public Animator animator;
    public Collider2D collider;
    public GameObject shadow;
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
        FlipSprite(bodySprite);

        if (isDead) { return; }
    }

    protected virtual void Move()
    {
        path.maxSpeed = moveSpeed;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (!isDead && distanceToPlayer > MoveToRange && !isAttacking && !playerAbilities.isGhosting) {
            isMoving = true; animator.SetBool("IsMoving", true);
            path.destination = player.transform.position;
        }
        else {
            isMoving = false; animator.SetBool("IsMoving", false);
            path.destination = transform.position;
        }
    }

    protected virtual void FlipSprite(SpriteRenderer sprite)
    {
        if (isDead || playerAbilities.isGhosting || isAttacking) { return; }

        if (player.transform.position.x > transform.position.x) { sprite.flipX = true; }
        else if (player.transform.position.x < transform.position.x) { sprite.flipX = false; }

        //if (path.desiredVelocity.x >= 0.01f) { sprite.flipX = true; }
        //else if (path.desiredVelocity.x <= -0.01f) { sprite.flipX = false; }
    }

    public virtual void Die()
    {
        isDead = true;
        waveManager.LiveEnemies.Remove(this.gameObject);
        collider.enabled = false;
        shadow.SetActive(false);
    }
}
