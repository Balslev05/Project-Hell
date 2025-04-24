using UnityEngine;
using Pathfinding;
using System.Collections;

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
    public bool isMoving;
    public bool inAttackRange;
    public bool canAttack;
    public bool isAttacking = false;

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

        if (distanceToPlayer <= attackRange) { inAttackRange = true; }
        else { inAttackRange = false; }

        if (inAttackRange && !isAttacking && !playerAbilities.isGhosting) { canAttack = true; }
        else { canAttack = false; }

        if (inAttackRange && canAttack && !isAttacking) {
            StartCoroutine(Attack());
        }
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

    protected virtual IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackSpeed);
        
        animator.SetTrigger("StopAttack");
        isAttacking = false;
        canAttack = true;
    }
}
