using System.Collections;
using UnityEngine;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float rollSpeed = 10f;
    [SerializeField] private float rollDuration = 1f;
    [SerializeField] private float rollCooldown = 1f;

    private Vector2 playerInput;

    private bool isMoving;
    private bool isRolling;
    private bool canRoll;
    private bool isDodgeing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isRolling = false;
        canRoll = true;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        LookAtMouse();
        Dodge();

        if (playerInput != Vector2.zero) { isMoving = true; }
        else { isMoving = false; }

        if (isRolling) { return; }
        GetPlayerInput();

        if (Input.GetKey(KeyCode.Space) && canRoll && isMoving)
            StartCoroutine(DodgeRoll());
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LookAtMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePosition - new Vector2(transform.position.x, transform.position.y);
    }

    public void GetPlayerInput()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        rb.linearVelocity = playerInput.normalized * moveSpeed;
    }

    private IEnumerator DodgeRoll()
    {
        isRolling = true;
        canRoll = false;
        moveSpeed = rollSpeed;
        Invincible(true);

        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
        moveSpeed = walkSpeed;
        Invincible(false);

        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isDodgeing)
        {
            Invincible(true);
            isDodgeing = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isDodgeing)
        {
            Invincible(false);
            isDodgeing = false;
        }
    }

    public void Invincible(bool becomeInvincible)
    {
        if (becomeInvincible) {
            collider.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            CameraShaker.Instance.ShakeOnce(3f, 3f, 1f, 1f);
        }
        else if (!becomeInvincible) {
            collider.enabled = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            CameraShaker.Instance.ShakeOnce(3f, 3f, 1f, 1f);
        }
    }
}
