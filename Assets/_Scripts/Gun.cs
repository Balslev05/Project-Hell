using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "GunSystem/gun", order = 1)]
public class Gun : ScriptableObject
{
    [Header("Assign")]
    public string gunname;
    public Sprite GunSprite;
    public GameObject projectilePrefab;
    public int price = 10;
    public string[] tags;
    [Header("Stats")]
    public int BaseDamage = 10;
    public int criticalchange = 10;
    public float criticalMultiplayer = 1.5f;
    public bool HoldToFire = false;
    public float timeBetweenShots;
    public int bulletCount = 1;    
    public float spreadAngle = 0f; 
    public int damage = 10;    
    public AmmoType ammoType;
    public enum AmmoType
    {
        Light,      // For pistols, SMGs
        Medium,     // For rifles, shotguns
        Heavy,      // For sniper rifles, heavy weapons
        Explosive,  // For rocket launchers, grenade launchers
        Shell       // For shotgun shells
    }
    public float CameraShakeStreangth = 0.5f;   
    public enum GunRarity { Common, Rare, Epic, Legendary }
    [Header("Meta")]    
    public GunRarity rarity = GunRarity.Common;

    private PlayerStats stats;
    

    public int GunSetup()
    {
        stats = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>();
        price = BaseDamage + criticalchange + (int)criticalMultiplayer + (int)timeBetweenShots + bulletCount - (int)spreadAngle + (int)damage;
        return price;
    }

}
