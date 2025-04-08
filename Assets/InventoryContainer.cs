using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    private GunManager gunManager;

    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
    }

    void Update()
    {
        
    }
}
