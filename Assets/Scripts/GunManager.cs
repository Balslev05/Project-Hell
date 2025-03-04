using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [Header("Assignebels")]
    public Transform shootPoint;
    public SpriteRenderer WeaponHolder;

    [Header("GunSystem")]
    public Gun currentGun;
    public int MaxGuns = 3;
    public List<Gun> GunList = new List<Gun>();

    [Header("GunStats")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTimer = 0f;
    private bool isReloading = false;
    private int currentGunIndex = 0;
    private float timeSinceLastShot = 0f;

    void Start()
    {
        if (GunList != null && GunList.Count > 0)
        {
            currentGun = GunList[0];
            currentAmmo = currentGun.magazin;
        }
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        
        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                isReloading = false;
                currentAmmo = currentGun.magazin;
            }
            return;
        }

        timeSinceLastShot += Time.deltaTime;

        if (currentGun && ((currentGun.HoldToFire && Input.GetKey(KeyCode.Mouse1)) || (!currentGun.HoldToFire && Input.GetKeyDown(KeyCode.Mouse1))))
        {
            if (currentAmmo > 0 && timeSinceLastShot >= currentGun.timeBetweenShots)
            {
                Fire(shootPoint);
                currentAmmo--;
                timeSinceLastShot = 0f;
            }
            else if (currentAmmo <= 0)
            {
                StartReload();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < currentGun.magazin)
        {
            StartReload();
        }

       /*  if (scrollInput > 0f)
        {
            SwitchToNextGun();
        }
        else if (scrollInput < 0f)
        {
            SwitchToPreviousGun();
        } */

        for (int i = 0; i < GunList.Count && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchToGun(i);
                WeaponHolder.sprite = currentGun.GunSprite;
            }
        }
    }

    private void StartReload()
    {
        if (!isReloading)
        {
            isReloading = true;
            reloadTimer = currentGun.reloadeTime;
            Debug.Log("Reloading...");
        }
    }

    private void SwitchToNextGun()
    {
        if (GunList == null || GunList.Count == 0) return;
        
        currentGunIndex = (currentGunIndex + 1) % GunList.Count;
        currentGun = GunList[currentGunIndex];
        currentAmmo = currentGun.magazin;
        isReloading = false;
    }

    private void SwitchToPreviousGun()
    {
        if (GunList == null || GunList.Count == 0) return;
        
        currentGunIndex--;
        if (currentGunIndex < 0) currentGunIndex = GunList.Count - 1;
        currentGun = GunList[currentGunIndex];
        currentAmmo = currentGun.magazin;
        isReloading = false;
    }

    private void SwitchToGun(int index)
    {
        if (GunList == null || index < 0 || index >= GunList.Count) return;
        
        currentGunIndex = index;
        currentGun = GunList[currentGunIndex];
        currentAmmo = currentGun.magazin;
        isReloading = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Got The Gun");
        Gun pickupGun = other.GetComponent<GunHolder>().gun;
        if (pickupGun != null && GunList.Count < MaxGuns)
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
