using System.Collections;
using UnityEngine;
using EZCameraShake;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAbilities abilities;
    private PlayerStats stats;
    
    private Managers manager;
    private CurrencyManager currencyManager;

    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float rollSpeed = 20f;
    [SerializeField] private float rollDuration = 1f;
    [SerializeField] private float rollCooldown = 1f;
    [SerializeField] private float SprintArmorCost;
    [SerializeField] private int rollArmorCost;

    private Vector2 playerInput;

    private bool isMoving;
    private bool isSprinting;
    private bool isRolling;
    private bool canRoll;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        abilities = GetComponent<PlayerAbilities>();
        stats = GetComponent<PlayerStats>();

        manager = GameObject.FindWithTag("Manager").GetComponent<Managers>();
        currencyManager = manager.currencyManager;

        isRolling = false;
        canRoll = true;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        //LookAtMouse();

        if (playerInput != Vector2.zero) { isMoving = true; animator.SetBool("IsMoving", true); }
        else { isMoving = false; animator.SetBool("IsMoving", false); }

        Sprint();

        if (isRolling) { return; }
        
        GetPlayerInput();

        if (Input.GetKey(KeyCode.Space) && canRoll && isMoving)
            StartCoroutine(DodgeRoll());
    }

    void FixedUpdate()
    {
        Move();

        if (isSprinting) {
            float armorCost = SprintArmorCost * Time.fixedDeltaTime;
            stats.LoseArmor(armorCost);
        }
    }

    /*private void LookAtMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePosition - new Vector2(transform.position.x, transform.position.y);
    }*/

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
        isRolling = true; animator.SetBool("IsRolling", true);
        canRoll = false;
        moveSpeed = rollSpeed;
        //abilities.GhostMode(true);
        stats.LoseArmor(rollArmorCost);

        yield return new WaitForSeconds(rollDuration);
        isRolling = false; animator.SetBool("IsRolling", false);
        moveSpeed = walkSpeed;
        //abilities.GhostMode(false);

        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isMoving && stats.currentArmor > 0) {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || !isMoving || stats.currentArmor <= 0) {
            isSprinting = false;
            moveSpeed = walkSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.tag == "Currency")
        {
            currencyManager.GetMoney(currencyManager.bananaPickupValue);
            Destroy(Collider.gameObject);
        }
    }
}
