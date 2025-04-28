using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private PlayerStats playerStats;

    [Header("Variables")]
    private float bulletDamage;
    private float bulletSpeed;
    [SerializeField] private int maxLifeTime;
    //[SerializeField] private float currentLifeTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        Destroy(this.gameObject, maxLifeTime);
    }

    private void FixedUpdate()
    {
        //currentLifeTime += Time.fixedDeltaTime;
        //if (currentLifeTime >= maxLifeTime) { Destroy(this.gameObject); }

        Vector2 movement = -(transform.right) * bulletSpeed;
        rb.linearVelocity = movement;
    }

    public void SetStats(float _bulletDamage, float _bulletSpeed)
    {
        bulletDamage = _bulletDamage;
        bulletSpeed = _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerStats.TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
