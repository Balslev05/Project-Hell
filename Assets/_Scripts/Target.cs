using UnityEngine;
using DG.Tweening;
public class Target : MonoBehaviour
{
    
    public int TempHealth = 100;
    public GameObject PopUpEffectPrefab;
    public GameObject CurrencyPrefab;
    public int AmountCurrency = 10;

    public void TakeDamage(int damage, float criticalMultiplayer)
    {
    //-----The calculations-----///
        TempHealth -= Mathf.FloorToInt(damage * criticalMultiplayer);
        CheckHealth(TempHealth);
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

    public Vector3 SetPopUpTransform()
    {
        float randomOffsetX = Random.Range(-1f, 1f);
        float randomOffsetY = Random.Range(-1f, 1f);

        return new Vector3(transform.localPosition.x + randomOffsetX, transform.localPosition.y * 1.5f + randomOffsetY, transform.localPosition.z);
    }

    public void CheckHealth(int damage)
    {
        if (TempHealth <= 0)
        {
            Debug.Log("Target Destroyed");
            Death();
        }
    }
    public void Death()
    {
        for (int i = 0; i < AmountCurrency; i++)
        {
            GameObject coin = Instantiate(CurrencyPrefab, transform.position, Quaternion.identity);

            Vector3 randomDirection = Random.insideUnitCircle.normalized * Random.Range(1.5f, 3f);
            Vector3 targetPosition = transform.position + randomDirection;

            coin.transform.localScale = Vector3.zero;
            coin.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

            coin.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutQuad);

            coin.transform.DORotate(new Vector3(0, 0, Random.Range(-180f, 180f)), 0.5f, RotateMode.FastBeyond360);

            // FADE OUT  MABY DELETE ?!?!?!?!?
            coin.GetComponent<SpriteRenderer>().DOFade(0, 3f).SetDelay(1f).OnComplete(() => Destroy(coin));
        }

    Destroy(gameObject);
}

}
