using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [Header("Card Info")]
    public Gun currentGun;
    public Image cardImage;
    public TMP_Text cardName;
    public TMP_Text tagsText;
    public TMP_Text[] tags;
    [Header("Stats")]
    public TMP_Text damage;
    public TMP_Text critChange;
    public TMP_Text critDamage;
    public TMP_Text fireRate;
    public TMP_Text price;

    private GunManager gunManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCard(Gun gun)
    {
        currentGun = gun;
        cardImage.sprite = gun.GunSprite;
        RectTransform rectTransform = cardImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(cardImage.sprite.rect.width, cardImage.sprite.rect.height);


        cardName.text = gun.name;
        tagsText.text = "Tags: ";
        for (int i = 0; i < gun.tags.Length; i++)
        {
            tags[i].text = gun.tags[i].ToString();
        }
        damage.text = "Damage: " + gun.damage.ToString();
        //critChange.text = "Crit Change: " + gun.critChance.ToString();
        //critDamage.text = "Crit Damage: " + gun.critDamage.ToString();
        fireRate.text = "Fire Rate: " + gun.timeBetweenShots.ToString();
        price.text = "Price: " + gun.price.ToString();
    }
    public void BuyGun()
    {
        gunManager.AddGun(currentGun);
        this.gameObject.SetActive(false);
    }
}
