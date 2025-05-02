using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Components")]
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private PlayerStats stats;

    [Header("Stats")]
    [SerializeField] private float ghostArmorCost;
    [HideInInspector] public bool isGhosting;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        Dodge();
    }

    void FixedUpdate()
    {
        if (isGhosting) {
            float armorCost = ghostArmorCost * Time.fixedDeltaTime;
            stats.LoseArmor(armorCost);
        }
    }

    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isGhosting && stats.currentArmor > 0) {
            GhostMode(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isGhosting || stats.currentArmor <= 0) {
            GhostMode(false);
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
}
