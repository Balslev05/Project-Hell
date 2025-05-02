using UnityEngine;
using System.Collections;

public class EnemyNormalDuck : EnemyBase
{
    [Header("NormalDuckSpecific")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackCooldown;

    private void Update()
    {
        base.Update();

        if (distanceToPlayer <= attackRange) { inAttackRange = true; }
        else { inAttackRange = false; }

        if (!isDead && inAttackRange && canAttack && !playerAbilities.isGhosting)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;

        HitboxLookAtPlayer();
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);

        animator.SetTrigger("StopAttack");
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void HitboxLookAtPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        attackHitbox.transform.up = playerPosition - new Vector2(transform.position.x, transform.position.y);
    }

    public void ActivateHitbox(bool activate)
    {
        if (activate) { attackHitbox.SetActive(true); }
        else if (!activate) { attackHitbox.SetActive(false); }
    }

    public override void Die()
    {
        base.Die();
        ActivateHitbox(false);
    }
}
