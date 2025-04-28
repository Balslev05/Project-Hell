using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GunManager : MonoBehaviour
{
    [Header("Assignments")]
    
    public Transform shootPoint;
    public SpriteRenderer WeaponHolder;
    public GameObject GunDrop;
    
    [Header("Gun System")]
    public Gun currentGun;
    public int MaxGuns = 3;
    public List<Gun> GunList = new List<Gun>();
    
    private float timeSinceLastShot = 0f;
    private int currentGunIndex = 0;

    public enum AmmoType { Light, Medium, Heavy, Explosive, Shell }
    private Dictionary<AmmoType, int> AmmoInventory = new Dictionary<AmmoType, int>();
    
    private void Awake()
    {
        foreach (AmmoType type in System.Enum.GetValues(typeof(AmmoType)))
        {
            AmmoInventory[type] = 0; 
        }
        AddAmmo(AmmoType.Light, 100);
        AddAmmo(AmmoType.Medium, 60);
        AddAmmo(AmmoType.Heavy, 20);
        AddAmmo(AmmoType.Explosive, 5);
        AddAmmo(AmmoType.Shell, 30);
    }

    public void AddAmmo(AmmoType type, int amount)
    {
        if (AmmoInventory.ContainsKey(type))
            AmmoInventory[type] += amount;
        else
            AmmoInventory[type] = amount;
    }
    
    private void Start()
    {
        if (GunList.Count > 0) currentGun = GunList[0];
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (currentGun && Input.GetKey(KeyCode.Mouse1) && timeSinceLastShot >= currentGun.timeBetweenShots)
        {
            AmmoType ammoType = (AmmoType)currentGun.ammoType;
            if (AmmoInventory[ammoType] > 0)
            {
                Fire(shootPoint);
                CameraShaker.Instance.ShakeOnce(currentGun.CameraShakeStreangth, currentGun.CameraShakeStreangth, 0.25f, 0.25f);
                AmmoInventory[ammoType]--;
                timeSinceLastShot = 0f;
            }
        }

     //   if (Input.GetKeyDown(KeyCode.Z)) DropGun(currentGun);

        for (int i = 0; i < GunList.Count && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) SwitchToGun(i);
        }
    }

    private void Fire(Transform firePoint)
    {
        for (int i = 0; i < currentGun.bulletCount; i++)
        {
            float angleOffset = Random.Range(-currentGun.spreadAngle / 2f, currentGun.spreadAngle / 2f);
            Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
            GameObject bullet = Instantiate(currentGun.projectilePrefab, firePoint.position, firePoint.rotation * rotation);
            bullet.GetComponent<bullet>().SetStats(currentGun.BaseDamage, RollForCritical());
        }
    }

    public float RollForCritical()
    {
        int RandomRoll = Random.Range(0, 101);

        if (RandomRoll <= currentGun.criticalchange)
        {
            return currentGun.criticalMultiplayer;
        }
        else
        {
            return 1f;
        }
    }

    private void SwitchToGun(int index)
    {
        if (index >= 0 && index < GunList.Count)
        {
            currentGunIndex = index;
            currentGun = GunList[currentGunIndex];
            WeaponHolder.sprite = currentGun.GunSprite;
        }
    }

    private void DropGun(Gun gun)
    {
        if (GunList.Count > 1)
        {
            GunList.Remove(gun);
            SwitchToGun(0);
            Instantiate(GunDrop, transform.position, Quaternion.identity).GetComponent<GunHolder>().gun = gun;
        }
    }
    public void SellGun(Gun gun)
    {
        if (gun == null) return;
        GunList.Remove(gun);
        SwitchToGun(0);
    }

    //!! DET HER ER PICK UP FUNKTIONEN
  /*   private void OnTriggerStay2D(Collider2D other)
    {
        Gun pickupGun = other.GetComponent<GunHolder>()?.gun;
        if (pickupGun != null && GunList.Count < MaxGuns && Input.GetKey(KeyCode.E))
        {
            GunList.Add(pickupGun);
            SwitchToGun(GunList.Count - 1);
            Destroy(other.gameObject);
        }
    } */

    public void AddGun(Gun gun)
    {
        if (GunList.Count < MaxGuns)
        {
            GunList.Add(gun);
            SwitchToGun(GunList.Count - 1);
        }else
        {
            Debug.Log("No more guns allowed!");
        }
    }

}
