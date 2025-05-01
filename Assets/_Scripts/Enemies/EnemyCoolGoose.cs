using UnityEngine;

public class EnemyCoolGoose : EnemyBase
{
    [Header("CoolGooseSpecific")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float HealthBuff = 2f;
    [SerializeField] private float DamageBuff = 2f;
    [SerializeField] private float SpeedBuff = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Buff enemies
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debuff enemies
    }
}
