using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rollSpeed = 12f;
    public float rollDuration = 0.2f;
    public float rollCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 rollDirection;
    private bool isRolling = false;
    private float rollTimer = 0f;
    private float rollCooldownTimer = 0f;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (!isRolling && Input.GetKeyDown(KeyCode.Space) && rollCooldownTimer <= 0f)
        {
            if (moveInput != Vector2.zero)
            {
                rollDirection = moveInput;
            }
            else
            {
                Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 toMouse = (mouseWorld - transform.position);
                rollDirection = toMouse.normalized;
            }

            isRolling = true;
            rollTimer = rollDuration;
            rollCooldownTimer = rollCooldown;
        }

        if (rollCooldownTimer > 0f)
            rollCooldownTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (isRolling)
        {
            rb.linearVelocity = rollDirection * rollSpeed;
            rollTimer -= Time.fixedDeltaTime;
            if (rollTimer <= 0f)
            {
                isRolling = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}