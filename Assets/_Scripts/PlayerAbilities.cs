using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Components")]
    private PlayerStats playerStats;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    [Header("Stats")]
    [HideInInspector] public bool isGhosting;
    [SerializeField] private float ghostArmorCost;
    [SerializeField] private float HealArmorCost;
    [SerializeField] private float HealAmount = 0.2f;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isGhosting && playerStats.currentArmor > 0) {
            GhostMode(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isGhosting || playerStats.currentArmor <= 0) {
            GhostMode(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && playerStats.currentArmor >= HealArmorCost && playerStats.currentHealth <= playerStats.maxHealth * (1 - HealAmount)) {
            Heal();
        }
    }

    void FixedUpdate()
    {
        if (isGhosting) {
            float armorCost = ghostArmorCost * Time.fixedDeltaTime;
            playerStats.LoseArmor(armorCost);
        }
    }

    public void GhostMode(bool becomeGhost)
    {
        if (becomeGhost) {
            isGhosting = true;
            //collider.enabled = false;
            this.gameObject.tag = "Untagged";
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
        }
        else if (!becomeGhost) {
            isGhosting = false;
            //collider.enabled = true;
            this.gameObject.tag = "Player";
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }

    private void StumpAbility() { }

    private void TimeControlAbility() { }

    private void Heal()
    {
        playerStats.HealHealth(Mathf.CeilToInt(playerStats.maxHealth * HealAmount));
        playerStats.LoseArmor(HealArmorCost);
    }

    private void Luck() { }
}
