using UnityEngine;

public class Target : MonoBehaviour
{
    
    public int TempHealth = 100;
    public GameObject PopUpEffectPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


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
        case float crit when crit > 0:
            EffectPrefab.SetRedGradient();
            EffectPrefab.ShowPopUp(damage.ToString(), Camera.main);
            break;
        case float crit when crit < 0:
            EffectPrefab.SetBadSilverGradient();
            EffectPrefab.ShowPopUp(damage.ToString(), Camera.main);
            break;
        default:
            EffectPrefab.SetWhiteGradient();
            EffectPrefab.ShowPopUp(damage.ToString(), Camera.main);
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
            Destroy(gameObject);
        }
    }
}
