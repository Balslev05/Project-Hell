using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "GunSystem/gun", order = 1)]
public class Gun : ScriptableObject
{
    [Header("Assign")]
    public Sprite GunSprite;
    public GameObject projectilePrefab;
    public int price = 10;
    public string[] tags;
    [Header("Stats")]
    public bool HoldToFire = false;
    public float timeBetweenShots;
    public int bulletCount = 1;    
    public float spreadAngle = 0f; 
    public int damage = 10;    
    public float ShakeStreangth = 0.5f;   
    public AmmoType ammoType;
     public enum AmmoType
    {
        Light,      // For pistols, SMGs
        Medium,     // For rifles, shotguns
        Heavy,      // For sniper rifles, heavy weapons
        Explosive,  // For rocket launchers, grenade launchers
        Shell       // For shotgun shells
    }
    

}
