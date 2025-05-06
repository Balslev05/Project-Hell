using UnityEngine;
using Pathfinding;
using System.Collections;
using UnityEngine.Rendering.VirtualTexturing;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Collider2D collider;
    public GameObject GFX;
    public SpriteRenderer bodySprite;
    public GameObject sunglasses;
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
    [SerializeField] public int maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] public float moveSpeed;
    [SerializeField] protected float MoveToRange;
    [SerializeField] public int damage;
    //[SerializeField] public int DeathVersions;

    protected float distanceToPlayer;
    public bool isDead;
    protected bool isMoving;
    protected bool inAttackRange;
    protected bool canAttack = true;
    protected bool isAttacking = false;
    [HideInInspector] public bool isBuffed;

    protected virtual void Start()
    {
        path = GetComponent<AIPath>();
        collider = GetComponent<Collider2D>();
        manager = GameObject.FindWithTag("Manager").GetComponent<Managers>();
        waveManager = manager.waveManager;
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    protected virtual void Update()
    {
        if(!player){
            player = GameObject.FindGameObjectWithTag("Player");
             playerAbilities = player.GetComponent<PlayerAbilities>();
            playerStats = player.GetComponent<PlayerStats>();
        }
        Move();
        FlipSprite();

        if (isDead) { return; }
    }

    protected virtual void Move()
    {
        path.maxSpeed = moveSpeed;
        Debug.Log(player);
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

    protected virtual void FlipSprite()
    {
        if (isDead || playerAbilities.isGhosting || isAttacking) { return; }

        if (player.transform.position.x > transform.position.x) {
            GFX.transform.localScale = new Vector3(-1, GFX.transform.localScale.y, GFX.transform.localScale.z);
        }
        else if (player.transform.position.x < transform.position.x) {
            GFX.transform.localScale = new Vector3(1, GFX.transform.localScale.y, GFX.transform.localScale.z);
        }

        //if (path.desiredVelocity.x >= 0.01f) { sprite.flipX = true; }
        //else if (path.desiredVelocity.x <= -0.01f) { sprite.flipX = false; }
    }

    public void ActivateSunglasses(bool activate)
    {
        if (activate) { sunglasses.SetActive(true); }
        else if (!activate) { sunglasses.SetActive(false); }
    }

    public virtual void Die()
    {
        isDead = true;
        waveManager.LiveEnemies.Remove(this.gameObject);
        tag = "Untagged";
        collider.enabled = false;
        shadow.SetActive(false);
        sunglasses.SetActive(false);
    }
}
