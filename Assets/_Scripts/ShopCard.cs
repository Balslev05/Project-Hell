using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopCard : MonoBehaviour
{
    public Item current_Item;
    public Gun current_Gun;

    [Header("Holders")]
    public GameObject GunBuyHolder;
    public GameObject ItemBuyHolder;

    [Header("Card Info GUN")]
    public Image cardImage;
    public TMP_Text cardName;
    public TMP_Text tagsText;
    public TMP_Text[] tags;
   
    [Header("GunStats")]
    [SerializeField] private GameObject GunstatsHolder;
    [SerializeField] private TMP_Text Text_1;
    [SerializeField] private TMP_Text Text_2;
    [SerializeField] private TMP_Text Text_3;
    [SerializeField] private TMP_Text Text_4;
    [SerializeField] private TMP_Text priceGUN;
    [Header("ItemStats")]
    public GameObject EffectHolder;
    public GameObject TextPrefab;
    [SerializeField] private TMP_Text priceItem;
    
    [SerializeField] Effect[] effects;

    [Header("RandomizeStats")]
    public int randomDamage;
    public int randomCritChange;
    public int randomCritDamage;
    public int randomFireRate;

    private GunManager gunManager;
    private PlayerStats playerStats;
    private Scrapper scrapper; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
        scrapper = GameObject.FindGameObjectWithTag("Scrapper").GetComponent<Scrapper>();
    }

    public void SetCard(Gun gun)
    {
        current_Gun = gun;
        cardImage.sprite = gun.GunSprite;
        cardName.text = gun.gunname;
        RectTransform rectTransform = cardImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(cardImage.sprite.rect.width, cardImage.sprite.rect.height);
        //RandomizeStats();
        SetGunStats();
    }
     public void SetCard(Item item)
    {
        current_Item = item;
        cardImage.sprite = item.icon;
        cardName.text = item.itemName;
        RectTransform rectTransform = cardImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(cardImage.sprite.rect.width, cardImage.sprite.rect.height);
        SetItemStats();
    }
    public void Buy()
    {
        if (current_Gun != null)
        {
            if (gunManager.GunList.Count >= gunManager.MaxGuns)
            {
                //shake effect to show and error
                transform.DOShakePosition(0.5f);
                this.GetComponent<Image>().DOColor(Color.red, 0.25f).OnComplete(() => this.GetComponent<Image>().DOColor(Color.black, 0.25f));
                return;
            }
            gunManager.AddGun(current_Gun);
            Debug.Log("Bought " + current_Gun.gunname);
            this.gameObject.SetActive(false);
            
        }
        else if(current_Item != null)
        {
            playerStats.Applyitem(current_Item);
            this.gameObject.SetActive(false);
        }   
    }
    public void GiveToScrapper()
    {
        scrapper.AddGun(current_Gun);
        gameObject.SetActive(false);
    }
   /*  private void RandomizeStats()
    {
        current_Gun.damage += Random.Range(-randomDamage, randomDamage);
        current_Gun.criticalchange += Random.Range(-randomCritChange, randomCritChange);
        current_Gun.criticalMultiplayer += Random.Range(-randomCritDamage, randomCritDamage);
        current_Gun.timeBetweenShots += Random.Range(-randomFireRate, randomFireRate);
    } */
    public void SetGunStats()
    {
        Text_1.text = "Damage: " + current_Gun.damage.ToString();
        Text_2.text = "Crit Change: " + current_Gun.criticalchange.ToString();
        Text_3.text = "Crit Damage: " + current_Gun.criticalMultiplayer.ToString();
        Text_4.text = "Fire Rate: " + current_Gun.timeBetweenShots.ToString();
        priceGUN.text = "Price:" + current_Gun.GunSetup();
        
        if (GunBuyHolder != null)
            GunBuyHolder.SetActive(true);
        if (GunBuyHolder != null)
            GunstatsHolder.SetActive(true);
        if (ItemBuyHolder != null)
            ItemBuyHolder.SetActive(false);
        if (EffectHolder != null)
        EffectHolder.SetActive(false);
    }
    public void SetItemStats()
    {   
        EffectHolder.SetActive(true);
        ItemBuyHolder.SetActive(true);

        GunBuyHolder.SetActive(false);
        GunstatsHolder.SetActive(false);
        Vector3 offset = new Vector3(0, -6, 0);

        foreach (Transform child in EffectHolder.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < current_Item.effects.Count; i++)
        {
            TMP_Text text = Instantiate(Text_1, EffectHolder.transform);
            text.transform.localPosition += offset;
            offset.y -= 6;
            text.gameObject.SetActive(true);
            text.text = current_Item.effects[i].Descreption();
        }
    }
}
