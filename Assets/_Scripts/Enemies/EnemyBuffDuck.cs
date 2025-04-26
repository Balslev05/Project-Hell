using UnityEngine;
using System.Collections;

public class EnemyBuffDuck : EnemyBase
{
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

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);

        animator.SetTrigger("StopAttack");
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
