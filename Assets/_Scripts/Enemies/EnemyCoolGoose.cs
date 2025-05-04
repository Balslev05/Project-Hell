using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyCoolGoose : EnemyBase
{
    [Header("CoolGooseSpecific")]
    [SerializeField] private GameObject buffAura;
    [SerializeField] private float range;
    public float healthBuff = 2f;
    public float damageBuff = 2f;
    public float speedBuff = 1.5f;
    [SerializeField] private float timer;

    private void Start()
    {
        base.Start();
        buffAura.transform.localScale = new Vector3 (range, range, 1);
        buffAura.SetActive(true);
        StartCoroutine(RotateAura());
    }

    private void Update()
    {
        if (isDead) { buffAura.SetActive(false); }
        base.Update();
    }

    private IEnumerator RotateAura()
    {
        buffAura.transform.DOLocalRotate(new Vector3(0, 0, 360), timer, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return new WaitForSeconds(timer);
        StartCoroutine(RotateAura());
    }
}
