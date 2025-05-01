using UnityEngine;

public class BuffAuraHitbox : MonoBehaviour
{
    [SerializeField] private EnemyCoolGoose coolGoose;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Buff enemies
        if (collision.tag == "Enemy")
        {
            collision.TryGetComponent<EnemyBase>(out EnemyBase enemy);
            if (enemy.isBuffed) { return; }

            enemy.maxHealth = Mathf.FloorToInt(enemy.maxHealth * coolGoose.healthBuff);
            enemy.currentHealth = enemy.currentHealth * coolGoose.healthBuff;
            enemy.damage = Mathf.FloorToInt(enemy.damage * coolGoose.damageBuff);
            enemy.moveSpeed = enemy.moveSpeed * coolGoose.speedBuff;
            enemy.isBuffed = true;
            enemy.ActivateSunglasses(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debuff enemies
        if (collision.tag == "Enemy")
        {
            collision.TryGetComponent<EnemyBase>(out EnemyBase enemy);
            if (!enemy.isBuffed) { return; }

            enemy.maxHealth = Mathf.FloorToInt(enemy.maxHealth / coolGoose.healthBuff);
            enemy.currentHealth = enemy.currentHealth / coolGoose.healthBuff;
            enemy.damage = Mathf.FloorToInt(enemy.damage / coolGoose.damageBuff);
            enemy.moveSpeed = enemy.moveSpeed / coolGoose.speedBuff;
            enemy.isBuffed = false;
            enemy.ActivateSunglasses(false);
        }
    }
}
