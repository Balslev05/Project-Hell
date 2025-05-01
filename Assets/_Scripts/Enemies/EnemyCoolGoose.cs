using UnityEngine;

public class EnemyCoolGoose : EnemyBase
{
    [Header("CoolGooseSpecific")]
    [SerializeField] private GameObject buffAura;
    [SerializeField] private float range;
    public float healthBuff = 2f;
    public float damageBuff = 2f;
    public float speedBuff = 1.5f;

    private void Start()
    {
        base.Start();
        buffAura.transform.localScale = new Vector3 (range, range, 1);
        buffAura.SetActive(true);
    }

    private void Update()
    {
        if (isDead) { buffAura.SetActive(false); }
        base.Update();
    }
}
