using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    [SerializeField] private Transform follow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = follow.transform.position;
    }
}
