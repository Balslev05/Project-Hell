using UnityEngine;

public class explosion : MonoBehaviour
{
    private PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Destroy(this.gameObject.GetComponent<Collider2D>(), 0.3f);
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Target>().TakeDamage(100*playerStats.BaseDamage,1);
        }
    }
}
