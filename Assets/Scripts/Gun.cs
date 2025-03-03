using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "GunSystem/gun", order = 1)]
public class Gun : ScriptableObject
{
    [Header("Assign")]
    public Sprite GunSprite;
    public GameObject projectilePrefab;
    [Header("Stats")]
    public bool HoldToFire = false;
    public int magazin;
    public float reloadeTime;
    public float timeBetweenShots;
    public int bulletCount = 1;    
    public float spreadAngle = 0f; 
    public int damage = 10;        
}
