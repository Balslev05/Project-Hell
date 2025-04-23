using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopCard : MonoBehaviour
{
    [Header("Card Info")]
    public Gun currentGun;
    public Image cardImage;
    public TMP_Text cardName;
    public TMP_Text tagsText;
    public TMP_Text[] tags;

    [Header("Stats")]
    [SerializeField] private TMP_Text damage;
    [SerializeField] private TMP_Text critChange;
    [SerializeField] private TMP_Text critDamage;
    [SerializeField] private TMP_Text fireRate;
    [SerializeField] private TMP_Text price;
    
    [Header("RandomizeStats")]
    public int randomDamage;
    public int randomCritChange;
    public int randomCritDamage;
    public int randomFireRate;

    private GunManager gunManager;
    private Scrapper scrapper; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
        scrapper = GameObject.FindGameObjectWithTag("Scrapper").GetComponent<Scrapper>();
    }

    public void SetCard(Gun gun)
    {
        currentGun = gun;
        cardImage.sprite = gun.GunSprite;
        RectTransform rectTransform = cardImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(cardImage.sprite.rect.width, cardImage.sprite.rect.height);


        cardName.text = gun.name;
        tagsText.text = "Tags: ";
        /*for (int i = 0; i < gun.tags.Length; i++)
        {
            tags[i].text = gun.tags[i].ToString();
        } */
        damage.text = "Damage: " + gun.damage.ToString();
        //critChange.text = "Crit Change: " + gun.critChance.ToString();
        //critDamage.text = "Crit Damage: " + gun.critDamage.ToString();
        fireRate.text = "Fire Rate: " + gun.timeBetweenShots.ToString();
        //price.text = "Price: " + gun.price.ToString();
        RandomizeStats();
        SetStats();

    }
    public void BuyGun()
    {
        if (gunManager.GunList.Count >= gunManager.MaxGuns)
        {
            // shake effect to show and error
            transform.DOShakePosition(0.5f);
            this.GetComponent<Image>().DOColor(Color.red, 0.25f).OnComplete(() => this.GetComponent<Image>().DOColor(Color.black, 0.25f));
            return;
        }
        gunManager.AddGun(currentGun);
        this.gameObject.SetActive(false);
    }
    public void GiveToScrapper()
    {
        scrapper.AddGun(currentGun);
        gameObject.SetActive(false);
    }
    private void RandomizeStats()
    {
        currentGun.damage += Random.Range(-randomDamage, randomDamage);
        currentGun.criticalchange += Random.Range(-randomCritChange, randomCritChange);
        currentGun.criticalMultiplayer += Random.Range(-randomCritDamage, randomCritDamage);
        currentGun.timeBetweenShots += Random.Range(-randomFireRate, randomFireRate);
    }
    public void SetStats()
    {
        damage.text = "Damage: " + currentGun.damage.ToString();
        critChange.text = "Crit Change: " + currentGun.criticalchange.ToString();
        critDamage.text = "Crit Damage: " + currentGun.criticalMultiplayer.ToString();
        fireRate.text = "Fire Rate: " + currentGun.timeBetweenShots.ToString();
        price.text = "Price:" + currentGun.CalculatePrice();
    }


}
