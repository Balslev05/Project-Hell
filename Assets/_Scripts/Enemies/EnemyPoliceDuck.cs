using UnityEngine;
using System.Collections;

public class EnemyPoliceDuck : EnemyBase
{
    [Header("PoliceDuckSpecific")]
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float bulletSpeed;


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

        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        bullet.GetComponent<EnemyBullet>().SetStats(damage, bulletSpeed);

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void GunLookAtPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        gun.transform.right = -(playerPosition - new Vector2(transform.position.x, transform.position.y));
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
