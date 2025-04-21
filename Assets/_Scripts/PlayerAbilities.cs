using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Components")]
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private PlayerStats Stats;

    [Header("Stats")]
    public float ghostArmorCost = 10;

    private bool isGhosting;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        Dodge();
    }

    void FixedUpdate()
    {
        if (isGhosting) {
            float armorCost = ghostArmorCost * Time.fixedDeltaTime;
            Stats.LoseArmor(armorCost);
        }
    }

    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isGhosting && Stats.currentArmor > 0) {
            GhostMode(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isGhosting || Stats.currentArmor <= 0) {
            GhostMode(false);
        }
    }

    public void GhostMode(bool becomeGhost)
    {
        if (becomeGhost) {
            isGhosting = true;
            collider.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
        }
        else if (!becomeGhost) {
            isGhosting = false;
            collider.enabled = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }
}
