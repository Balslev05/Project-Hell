using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GunManager : MonoBehaviour
{
    [Header("Assignebels")]
    public Transform shootPoint;
    public SpriteRenderer WeaponHolder;
    public GameObject GunDrop;
    [Header("GunSystem")]
    public Gun currentGun;
    public int MaxGuns = 3;
    public List<Gun> GunList = new List<Gun>();
    [Header("GunStats")]
    private float timeSinceLastShot = 0f;
    private int currentGunIndex = 0;
    
    public enum AmmoType
    {
        Light,      // For pistols, SMGs
        Medium,     // For rifles, shotguns
        Heavy,      // For sniper rifles, heavy weapons
        Explosive,  // For rocket launchers, grenade launchers
        Shell       // For shotgun shells
    }

    [Header("Ammo")]
    [SerializeField] private int _lightAmmo = 100;
    [SerializeField] private int _mediumAmmo = 60;
    [SerializeField] private int _heavyAmmo = 20;
    [SerializeField] private int _explosiveAmmo = 5;
    [SerializeField] private int _shellAmmo = 30;

    private Dictionary<AmmoType, int> AmmoInventory;

    public int lightAmmo 
    {
        get { return _lightAmmo; }
        set { _lightAmmo = value; UpdateAmmoInventory(); }
    }
    public int mediumAmmo
    {
        get { return _mediumAmmo; }
        set { _mediumAmmo = value; UpdateAmmoInventory(); }
    }
    public int heavyAmmo
    {
        get { return _heavyAmmo; }
        set { _heavyAmmo = value; UpdateAmmoInventory(); }
    }
    public int explosiveAmmo
    {
        get { return _explosiveAmmo; }
        set { _explosiveAmmo = value; UpdateAmmoInventory(); }
    }
    public int shellAmmo
    {
        get { return _shellAmmo; }
        set { _shellAmmo = value; UpdateAmmoInventory(); }
    }

    private void UpdateAmmoInventory()
    {
        if (AmmoInventory != null)
        {
            AmmoInventory[AmmoType.Light] = _lightAmmo;
            AmmoInventory[AmmoType.Medium] = _mediumAmmo;
            AmmoInventory[AmmoType.Heavy] = _heavyAmmo;
            AmmoInventory[AmmoType.Explosive] = _explosiveAmmo;
            AmmoInventory[AmmoType.Shell] = _shellAmmo;
        }
    }

    private void Awake()
    {
        AmmoInventory = new Dictionary<AmmoType, int>()
        {
            {AmmoType.Light, _lightAmmo},
            {AmmoType.Medium, _mediumAmmo},
            {AmmoType.Heavy, _heavyAmmo},
            {AmmoType.Explosive, _explosiveAmmo},
            {AmmoType.Shell, _shellAmmo}
        };
    }

    void Start()
    {
        if (GunList != null && GunList.Count > 0)
        {
            currentGun = GunList[0];
        }
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        timeSinceLastShot += Time.deltaTime;

        if (currentGun && ((currentGun.HoldToFire && Input.GetKey(KeyCode.Mouse1)) || (!currentGun.HoldToFire && Input.GetKeyDown(KeyCode.Mouse1))))
        {
            AmmoType currentAmmoType = (AmmoType)currentGun.ammoType;
            
            if (AmmoInventory[currentAmmoType] > 0 && timeSinceLastShot >= currentGun.timeBetweenShots)
            {
                Fire(shootPoint);
                CameraShaker.Instance.ShakeOnce(currentGun.ShakeStreangth, currentGun.ShakeStreangth, 0.25f, 0.25f);
                
                switch(currentAmmoType)
                {
                    case AmmoType.Light:
                        lightAmmo--;
                        break;
                    case AmmoType.Medium:
                        mediumAmmo--;
                        break;
                    case AmmoType.Heavy:
                        heavyAmmo--;
                        break;
                    case AmmoType.Explosive:
                        explosiveAmmo--;
                        break;
                    case AmmoType.Shell:
                        shellAmmo--;
                        break;
                }
                
                timeSinceLastShot = 0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            {
                DropGun(currentGun);
            }
        }

        for (int i = 0; i < GunList.Count && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchToGun(i);
                WeaponHolder.sprite = currentGun.GunSprite;
            }
        }
   
    }

    private void SwitchToNextGun()
    {
        if (GunList == null || GunList.Count == 0) return;
        
        currentGunIndex = (currentGunIndex + 1) % GunList.Count;
        currentGun = GunList[currentGunIndex];
    }

    private void SwitchToPreviousGun()
    {
        if (GunList == null || GunList.Count == 0) return;
        
        currentGunIndex--;
        if (currentGunIndex < 0) currentGunIndex = GunList.Count - 1;
        currentGun = GunList[currentGunIndex];
    }

    private void SwitchToGun(int index)
    {
        if (GunList == null || index < 0 || index >= GunList.Count) return;
        
        currentGunIndex = index;
        currentGun = GunList[currentGunIndex];
    }

    private void DropGun(Gun gun)
    {
        if (GunList.Count > 1) // Don't drop if it's the last gun
                {
                    GunList.Remove(gun);
                    currentGunIndex = 0;
                    currentGun = GunList[currentGunIndex];
                    WeaponHolder.sprite = currentGun.GunSprite;
                    GameObject DroppedGun = Instantiate(GunDrop,transform.position,Quaternion.identity);
                    DroppedGun.GetComponent<GunHolder>().gun = gun;
                    DroppedGun.GetComponent<GunHolder>().Activate();
                }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Got The Gun");
        Gun pickupGun = other.GetComponent<GunHolder>().gun;
        if (pickupGun != null && GunList.Count < MaxGuns && Input.GetKey(KeyCode.E))
        {
            GunList.Add(pickupGun);
            
            SwitchToGun(GunList.Count - 1);

            other.enabled = false;
            if (other.GetComponent<SpriteRenderer>())
            {
                other.GetComponent<SpriteRenderer>().enabled = false;
            }
            WeaponHolder.sprite = currentGun.GunSprite;
            Destroy(other.gameObject);
        }
    }

    public void Fire(Transform firePoint) 
    {
        for (int i = 0; i < currentGun.bulletCount; i++)
        {
            float angleOffset = Random.Range(-currentGun.spreadAngle / 2f, currentGun.spreadAngle / 2f);
            Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
            Vector2 direction = (rotation * firePoint.right).normalized;
            GameObject newBullet = Instantiate(currentGun.projectilePrefab, firePoint.position, firePoint.rotation * rotation);
        }
    }
}
