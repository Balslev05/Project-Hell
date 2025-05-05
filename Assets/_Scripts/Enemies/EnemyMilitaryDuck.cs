using UnityEngine;
using System.Collections;
using System.Net;


public class EnemyMilitaryDuck : EnemyBase
{
    [Header("MilitaryDuckSpecific")]
    [SerializeField] protected int maxArmor;
    [HideInInspector] public float currentArmor;
    public bool Taunted = false;
    private bool Taunting = false;
    [SerializeField] private float TauntingDuration;

    [Header("Revolver")]
    [SerializeField] private GameObject revolver;
    [SerializeField] private SpriteRenderer revolverSprite;
    [SerializeField] private Transform revolverPoint;
    [SerializeField] private GameObject revolverBulletPrefab;
    [SerializeField] private int revolverBulletDamage;
    [SerializeField] private float revolverBulletSpeed;
    [SerializeField] private float revolverRange;
    [SerializeField] private float revolverShootCooldown;
    [SerializeField] private int revolverRapidFireAmount;
    [SerializeField] private float revolverRapidFireCooldown;
    private float revolverOffsetX = -0.125f;
    //private float revolverOffsetY = -0.1875f;

    [Header("Sniper")]
    [SerializeField] private GameObject sniper;
    [SerializeField] private SpriteRenderer sniperSprite;
    [SerializeField] private Transform sniperPoint;
    [SerializeField] private GameObject sniperBulletPrefab;
    [SerializeField] private int sniperBulletDamage;
    [SerializeField] private float sniperBulletSpeed;
    [SerializeField] private float sniperRange;
    [SerializeField] private float sniperShootCooldown;
    private float sniperOffsetX = -0.15625f;
    //private float sniperOffsetY = -0.09375f;

    private void Start()
    {
        base.Start();
        currentArmor = maxArmor;

        ActivateRevolver(true);
        ActivateSniper(false);
    }

    private void Update()
    {
        if (Taunting) { path.destination = transform.position; return; }

        base.Update();

        GunLookAtPlayer();

        if (!Taunted && distanceToPlayer <= revolverRange) { inAttackRange = true; }
        else if (Taunted && distanceToPlayer <= sniperRange) { inAttackRange = true; }
        else { inAttackRange = false; }

        if (!isDead && inAttackRange && canAttack && !playerAbilities.isGhosting) {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        if (!Taunted) {
            canAttack = false;
            for (int i = 0; i < revolverRapidFireAmount; i++)
            {
                GameObject bullet = Instantiate(revolverBulletPrefab, revolverPoint.position, revolverPoint.rotation);
                bullet.GetComponent<EnemyBullet>().SetStats(revolverBulletDamage, revolverBulletSpeed);
                yield return new WaitForSeconds(revolverRapidFireCooldown);
            }

            yield return new WaitForSeconds(revolverShootCooldown);
            canAttack = true;
        }
        else if (Taunted) {
            canAttack = false;
            GameObject bullet = Instantiate(sniperBulletPrefab, sniperPoint.position, sniperPoint.rotation);
            bullet.GetComponent<EnemyBullet>().SetStats(sniperBulletDamage, sniperBulletSpeed);

            yield return new WaitForSeconds(sniperShootCooldown);
            canAttack = true;
        }
    }

    private void GunLookAtPlayer()
    {
        Vector2 playerPosition = player.transform.position;

        if (!Taunted) {
            revolver.transform.right = -(playerPosition - new Vector2(transform.position.x, transform.position.y));

            if (player.transform.position.x > transform.position.x) {
                revolver.transform.localScale = new Vector3(revolver.transform.localScale.x, -1, revolver.transform.localScale.z);
                revolver.transform.localPosition = new Vector3(revolverOffsetX * -1, revolver.transform.localPosition.y, revolver.transform.localPosition.z);
            }
            else if (player.transform.position.x < transform.position.x) {
                revolver.transform.localScale = new Vector3(revolver.transform.localScale.x, 1, revolver.transform.localScale.z);
                revolver.transform.localPosition = new Vector3(revolverOffsetX, revolver.transform.localPosition.y, revolver.transform.localPosition.z);
            }
        }
        else if (Taunted) {
            sniper.transform.right = -(playerPosition - new Vector2(transform.position.x, transform.position.y));

            if (player.transform.position.x > transform.position.x) {
                sniper.transform.localScale = new Vector3(sniper.transform.localScale.x, -1, sniper.transform.localScale.z);
                sniper.transform.localPosition = new Vector3(sniperOffsetX * -1, sniper.transform.localPosition.y, sniper.transform.localPosition.z);
            }
            else if (player.transform.position.x < transform.position.x) {
                sniper.transform.localScale = new Vector3(sniper.transform.localScale.x, 1, sniper.transform.localScale.z);
                sniper.transform.localPosition = new Vector3(sniperOffsetX, sniper.transform.localPosition.y, sniper.transform.localPosition.z);
            }
        }
    }

    private void ActivateRevolver(bool activate)
    {
        if (activate) { revolver.SetActive(true); }
        else if (!activate) { revolver.SetActive(false); }
    }

    private void ActivateSniper(bool activate)
    {
        if (activate) { sniper.SetActive(true); }
        else if (!activate) { sniper.SetActive(false); }
    }

    public IEnumerator Taunt()
    {
        Taunting = true;
        ActivateRevolver(false);
        collider.enabled = false;
        animator.SetTrigger("Taunt");
        yield return new WaitForSeconds(TauntingDuration);

        ActivateSniper(true);
        collider.enabled = true;
        animator.SetTrigger("Continue");
        Taunting = false;
        Taunted = true;
    }

    public override void Die()
    {
        base.Die();
        ActivateRevolver(false);
        ActivateSniper(false);
    }
}
