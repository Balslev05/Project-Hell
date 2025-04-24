using UnityEngine;
using System.Collections;

public class EnemyNormalDuck : EnemyBase
{
    [Header("NormalDuckSpecific")]
    [SerializeField] private GameObject attackHitbox;

    private void Update()
    {
        base.Update();

        if (distanceToPlayer <= attackRange) { inAttackRange = true; }
        else { inAttackRange = false; }

        if (inAttackRange && !isAttacking && !playerAbilities.isGhosting) { canAttack = true; }
        else { canAttack = false; }

        if (inAttackRange && canAttack && !isAttacking)
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
}
