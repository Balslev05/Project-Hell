using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform shootPoint;
    public Gun currentGun;
    public int MaxGuns = 3;
    public List<Gun> GunList = new List<Gun>();
    private int currentGunIndex = 0;

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
        
        if (currentGun && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Fire(shootPoint);
        }
        if (scrollInput > 0f)
        {
            SwitchToNextGun();
        }
        else if (scrollInput < 0f)
        {
            SwitchToPreviousGun();
        }

        for (int i = 0; i < GunList.Count && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchToGun(i);
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
            Destroy(other.gameObject);
        }
    }
    public void Fire(Transform firePoint) 
    {
        Debug.Log("BANG BANG");
        for (int i = 0; i < currentGun.bulletCount; i++) {

            float angleOffset = Random.Range(-currentGun.spreadAngle / 2f, currentGun.spreadAngle / 2f);
            Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
            Vector2 direction = (rotation * firePoint.right).normalized;

            if (currentGun.HitScan)
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, currentGun.raycastMaxDistance);
                
                if (hit.collider != null)
                {
                    Debug.DrawLine(firePoint.position, hit.point, currentGun.raycastColor, currentGun.raycastDuration);
                    var hitObject = hit.collider.gameObject;
                    // hitObject.GetComponent<Health>().TakeDamage(damage);
                }
                else
                {
                    Debug.DrawLine(firePoint.position, (Vector2)firePoint.position + direction * currentGun.raycastMaxDistance, currentGun.raycastColor, currentGun.raycastDuration);
                }
            }
            else {
                GameObject newBullet = Instantiate(currentGun.projectilePrefab, firePoint.position, firePoint.rotation * rotation);
            }
        }
        Debug.Log("No more BANG BANG");
    }
}
