using UnityEngine;
using System.Collections;

public class EnemyPoliceDuck : EnemyBase
{
    [Header("PoliceDuckSpecific")]
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected float attackRange;
    [SerializeField] private float attackCooldown;

    private void Update()
    {
        base.Update();

        GunLookAtPlayer();

        if (distanceToPlayer <= attackRange) { inAttackRange = true; }
        else { inAttackRange = false; }

        if (inAttackRange && canAttack && !playerAbilities.isGhosting)
        {
            StartCoroutine(Shot());
        }
    }

    private IEnumerator Shot()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void GunLookAtPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        gun.transform.up = playerPosition - new Vector2(transform.position.x, transform.position.y);
    }

    public void ActivateGun(bool activate)
    {
        if (activate) { gun.SetActive(true); }
        else if (!activate) { gun.SetActive(false); }
    }

    public override void Die()
    {
        base.Die();
        ActivateGun(false);
    }
}
