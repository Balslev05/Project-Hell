using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    private Vector2 playerInput;

    [Header("Stats")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float rollSpeed = 10;
    //[SerializeField] private float rollCooldown = 1f;
    private bool isMoving;
    private bool isDodgeing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDodgeing = false;
    }

    void Update()
    {
        LookAtMouse();

        if (isDodgeing) { return; }
        GetPlayerInput();
    }

    void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !isDodgeing)
            Dodge();
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
        rb.linearVelocity = playerInput.normalized * speed;
    }

    private void Dodge()
    {
        isDodgeing = true;
        rb.linearVelocity = playerInput.normalized * rollSpeed;
    }
}
