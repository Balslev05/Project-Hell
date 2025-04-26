using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Guns")]
    public Gun[] guns;
    public Item[] items;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public Gun GetRandomGun()
    {
        return guns[Random.Range(0, guns.Length)];
    }
    public Item GetRandomItem()
    {
        return items[Random.Range(0, items.Length)];
    }
}
