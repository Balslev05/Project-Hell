using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;
public class Target : MonoBehaviour
{
    private EnemyBase enemyBase;
    private EnemyMilitaryDuck militaryDuck;
    private EnemyPoliceDuck policeDuck;

    [SerializeField] private GameObject PopUpEffectPrefab;
    [SerializeField] private GameObject CurrencyPrefab;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        if (enemyBase.GetComponent<EnemyMilitaryDuck>()) { militaryDuck = enemyBase.GetComponent<EnemyMilitaryDuck>(); }
        if (enemyBase.GetComponent<EnemyPoliceDuck>()) { policeDuck = enemyBase.GetComponent<EnemyPoliceDuck>(); }
    }

    public void TakeDamage(int damage, float criticalMultiplayer)
    {
        //-----The calculations-----///
        if (militaryDuck != null && militaryDuck.currentArmor > 0) {
            militaryDuck.currentArmor -= Mathf.FloorToInt(damage * criticalMultiplayer);
        }
        else {
            enemyBase.currentHealth -= Mathf.FloorToInt(damage * criticalMultiplayer);
        }
        CheckHealth();
     //-----SpawnDamageNumbers-----///
        GameObject worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");
        PopUp EffectPrefab  = Instantiate(PopUpEffectPrefab, worldCanvas.transform).GetComponent<PopUp>();
        EffectPrefab.transform.localPosition = SetPopUpTransform();

        switch (criticalMultiplayer)
        {
        case 1:
            EffectPrefab.SetWhiteGradient();
            EffectPrefab.ShowPopUp((damage).ToString(), Camera.main);
            break;
        case > 1:
            EffectPrefab.SetRedGradient();
            EffectPrefab.ShowPopUp((damage*criticalMultiplayer).ToString(), Camera.main);
            break;
        case < 0:
            EffectPrefab.SetBadSilverGradient();
            EffectPrefab.ShowPopUp((damage*criticalMultiplayer).ToString(), Camera.main);
            break;
        default:
            EffectPrefab.SetWhiteGradient();
            EffectPrefab.ShowPopUp((damage).ToString(), Camera.main);
            break;
        }
    }

    private Vector3 SetPopUpTransform()
    {
        float randomOffsetX = Random.Range(-1f, 1f);
        float randomOffsetY = Random.Range(-1f, 1f);

        return new Vector3(transform.localPosition.x + randomOffsetX, transform.localPosition.y * 1.5f + randomOffsetY, transform.localPosition.z);
    }

    private void CheckHealth()
    {
        if (militaryDuck != null && militaryDuck.currentArmor <= 0) { StartCoroutine(militaryDuck.Taunt()); }
        if (enemyBase.currentHealth <= 0) { StartCoroutine(Die()); }
    }

    private IEnumerator Die()
    {
        if (militaryDuck != null) { militaryDuck.Die(); }
        else if (policeDuck != null) { policeDuck.Die(); }
        else { enemyBase.Die(); }

        for (int i = 0; i < enemyBase.currencyValue; i++)
        {
            GameObject coin = Instantiate(CurrencyPrefab, transform.position, Quaternion.identity);

            Vector3 randomDirection = Random.insideUnitCircle.normalized * Random.Range(1.5f, 3f);
            Vector3 targetPosition = transform.position + randomDirection;

            coin.transform.localScale = Vector3.zero;
            coin.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

            coin.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutQuad);

            coin.transform.DORotate(new Vector3(0, 0, Random.Range(-180f, 180f)), 0.5f, RotateMode.FastBeyond360);

            // FADE OUT  MABY DELETE ?!?!?!?!?
            //coin.GetComponent<SpriteRenderer>().DOFade(0, 3f).SetDelay(1f).OnComplete(() => Destroy(coin));
        }

        enemyBase.animator.SetTrigger("Die");
        yield return new WaitForSeconds(10);
        enemyBase.bodySprite.DOFade(0, 3f).SetDelay(1f).OnComplete(() => Destroy(this.gameObject));
    }
}
