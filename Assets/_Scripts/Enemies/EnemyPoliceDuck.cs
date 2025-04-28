using UnityEngine;
using System.Collections;

public class EnemyPoliceDuck : EnemyBase
{
    [Header("PoliceDuckSpecific")]
    [SerializeField] private GameObject gun;
    [SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float range;
    [SerializeField] private float shootCooldown;

    private void Update()
    {
        base.Update();

        GunLookAtPlayer();

        if (distanceToPlayer <= range) { inAttackRange = true; }
        else { inAttackRange = false; }

        if (!isDead && inAttackRange && canAttack && !playerAbilities.isGhosting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        canAttack = false;

        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        bullet.GetComponent<EnemyBullet>().SetStats(damage, bulletSpeed);

        yield return new WaitForSeconds(shootCooldown);
        canAttack = true;
    }

    private void GunLookAtPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        gun.transform.right = -(playerPosition - new Vector2(transform.position.x, transform.position.y));

        if (player.transform.position.x > transform.position.x) { gunSprite.flipY = true; }
        else if (player.transform.position.x < transform.position.x) { gunSprite.flipY = false; }
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
