using UnityEngine;
using System.Collections;

public class EnemyBuffDuck : EnemyBase
{
    [Header("BuffDuckSpecific")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDuration;
    [SerializeField] protected float attackCooldown;

    private void Update()
    {
        if (isDead) { ActivateHitbox(false); }

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

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);

        animator.SetTrigger("StopAttack");
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void ActivateHitbox(bool activate)
    {
        if (activate) { attackHitbox.SetActive(true); }
        else if (!activate) { attackHitbox.SetActive(false); }
    }
}
