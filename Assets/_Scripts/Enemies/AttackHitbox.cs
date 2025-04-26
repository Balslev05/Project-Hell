using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    [SerializeField] private EnemyBase enemyBase;
    private GameObject player;
    private PlayerStats playerStats;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    public void Active() {  hitbox.SetActive(true); }
    public void Deactive() {  hitbox.SetActive(false); }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerStats.TakeDamage(enemyBase.damage);
        }
    }
}
