using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "GunSystem/gun", order = 1)]
public class Gun : ScriptableObject
{
    [Header("Assign")]
    public GameObject projectilePrefab;
    [Header("Stats")]
    public float fireRate = 0.5f;  
    public int damage = 10;        
    public int bulletCount = 1;    
    public float spreadAngle = 0f; 
    public bool HitScan;

    [Header("Raycast Visual Settings")]
    public Color raycastColor = Color.red;
    public float raycastDuration = 0.1f;
    public float raycastWidth = 0.1f;
    public float raycastMaxDistance = 100f;

}
