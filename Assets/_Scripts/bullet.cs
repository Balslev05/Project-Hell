    using UnityEngine;

public class bullet : MonoBehaviour
{
    //[Header("Bullet Unqiue Stats")]
    [HideInInspector] public int bulletDamage;
    [HideInInspector] public float criticalMultiplayer = 0;
    [Header("Basic Movement")]
    public float speed = 5f;
    public bool randomizeSpeed = false;
    public float minSpeed = 3f;
    public float maxSpeed = 8f;
    private float currentSpeed;

    [Header("Rotation")]
    public float rotationSpeed = 0f;
    public bool randomizeRotation = false;
    public float minRotation = -360f;
    public float maxRotation = 360f;

    [Header("Wave Motion Bullet hell classic")]
    public float sinAmplitude = 0f;
    public float sinFrequency = 0f;
    public bool oscillatingAmplitude = false;
    public float amplitudeChangeSpeed = 1f;

    [Header("Homing")]
    public bool homing = false;
    public float homingStrength = 2f;
    public string homingTarget;
    public bool delayedHoming = false;
    public float homingDelay = 1f;
    
    [Header("LifeTime")]
    public int maxLifeTime;

    [Header("Collision Effects")]
    public GameObject spawnOnHit;

    private Vector2 direction;
    private float lifetime = 0f;
    private Transform target;
    private Rigidbody2D rb;
    private float currentAmplitude;

    public void SetStats(int _bulletdamage, float _critimultiplayer)
    {
        bulletDamage = _bulletdamage;
        criticalMultiplayer = _critimultiplayer;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.right;
        // Initialize speeds
        currentSpeed = randomizeSpeed ? Random.Range(minSpeed, maxSpeed) : speed;
        if (randomizeRotation) {
            rotationSpeed = Random.Range(minRotation, maxRotation);
        }
        currentAmplitude = sinAmplitude;

        
        if (homing) {
            target = GameObject.FindGameObjectWithTag(homingTarget)?.transform;
        }
        Destroy(this.gameObject,maxLifeTime);
    }

    void FixedUpdate()
    {
        lifetime += Time.fixedDeltaTime;

        Vector2 movement = direction * currentSpeed;
        
        if (currentAmplitude > 0) {
            float perpendicular = Mathf.Sin(lifetime * sinFrequency) * currentAmplitude;
            movement += new Vector2(-direction.y, direction.x) * perpendicular;

            
            if (oscillatingAmplitude) {
                currentAmplitude = sinAmplitude * Mathf.Abs(Mathf.Sin(lifetime * amplitudeChangeSpeed));
            }
        }
        
        if (homing && target != null) {
            if (!delayedHoming || lifetime > homingDelay) {
                Vector2 toTarget = (target.position - transform.position).normalized;
                direction = Vector2.Lerp(direction, toTarget, homingStrength * Time.deltaTime);
            }
        }
        
        if (rotationSpeed != 0) {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
        rb.linearVelocity = movement;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit" + bulletDamage);
            other.gameObject.GetComponent<Target>().TakeDamage(bulletDamage,criticalMultiplayer);
            Destroy(gameObject);
        }
        if (spawnOnHit != null)
        {
            Instantiate(spawnOnHit, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawnOnHit != null)
        {
            Instantiate(spawnOnHit, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
