using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ammo : MonoBehaviour
{
    private GunManager gunmanager;

    [Header("Assignebels")]
    [SerializeField] private Image gunImage;
    [SerializeField] private Image AmmoIcon;
    [SerializeField] private TMP_Text AmmoText;

    [Header("Icons")]
    [SerializeField] private Sprite lightAmmo;
    [SerializeField] private Sprite mediumAmmo; 
    [SerializeField] private Sprite heavyAmmo; 
    [SerializeField] private Sprite EksplosiveAmmo; 
    [SerializeField] private Sprite shellsAmmo; 
    [Header("Colors")]
    [SerializeField] private Color lightColor;
    [SerializeField] private Color mediumColor; 
    [SerializeField] private Color heavyColor; 
    [SerializeField] private Color EksplosiveColor; 
    [SerializeField] private Color shellsColor; 
    


    void Awake()
    {
        gunmanager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
    }


    public void UpdateUI(Gun currentGun)
    {
        if(this.gameObject.activeInHierarchy)
        {
            switch (currentGun.ammoType)
            {
                case Gun.AmmoType.Light:
                gunImage.sprite = currentGun.GunSprite;
                AmmoIcon.sprite = lightAmmo;
                AmmoText.text = gunmanager.GetAmmoCount("Light");
                AmmoText.color = lightColor;
                break;

                case Gun.AmmoType.Medium:
                gunImage.sprite = currentGun.GunSprite;
                AmmoIcon.sprite = mediumAmmo;
                AmmoText.text = gunmanager.GetAmmoCount("Medium");
                AmmoText.color = mediumColor;
                break;

                case Gun.AmmoType.Heavy:
                gunImage.sprite = currentGun.GunSprite;
                AmmoIcon.sprite = heavyAmmo;
                AmmoText.text = gunmanager.GetAmmoCount("Heavy");
                AmmoText.color = heavyColor;

                break;

                case Gun.AmmoType.Explosive:
                gunImage.sprite = currentGun.GunSprite;
                AmmoIcon.sprite = EksplosiveAmmo;
                AmmoText.text = gunmanager.GetAmmoCount("Explosive");
                AmmoText.color = EksplosiveColor;

                break;

                case Gun.AmmoType.Shell:
                gunImage.sprite = currentGun.GunSprite;
                AmmoIcon.sprite = shellsAmmo;
                AmmoText.text = gunmanager.GetAmmoCount("Shell");
                AmmoText.color = shellsColor;
                break;

                default:

                break;
            }
        }
    }
}
