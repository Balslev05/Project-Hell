using System.Collections;
using UnityEngine;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private PlayerAbilities abilities;
    private PlayerStats stats;

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

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        abilities = GetComponent<PlayerAbilities>();
        stats = GetComponent<PlayerStats>();

        isRolling = false;
        canRoll = true;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        LookAtMouse();

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
        abilities.GhostMode(true);
        stats.LoseArmor(5);

        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
        moveSpeed = walkSpeed;
        abilities.GhostMode(false);

        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }
}
