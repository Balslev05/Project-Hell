using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Guns")]
    public Gun[] guns;

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
}
