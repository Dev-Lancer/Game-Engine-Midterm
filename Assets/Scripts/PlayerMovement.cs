using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    public float speed = 7.0f;
    public float acceleration = 50.0f;
    public float deceleration = 60.0f;

    [Header("Jump")]
    public float jumpPow = 10.0f;

    [Header("Fast Fall")]
    public float fallSpeed = 20.0f;

    private float hori;

    private bool isFaceRight = true;
    private bool jumpRequested = false;
    private bool jumpCutRequested = false;
    private int healthPoint = 0;
    public bool jumpCheck;
    public GameManager manager;
    public GameObject respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        rBody = GetComponent<Rigidbody2D>();
        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        hori = 0.0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            hori = -1.0f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            hori = 1.0f;
        }

        // Check if player is on the ground


        // Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }

        // Short hop when releasing Space
        if (Keyboard.current.spaceKey.wasReleasedThisFrame &&
            rBody.linearVelocity.y > 0.0f)
        {
            jumpCutRequested = true;
        }

        // Fast fall
        if (Keyboard.current.sKey.isPressed && !IsGrounded())
        {
            rBody.linearVelocity = new Vector2(
                rBody.linearVelocity.x,
                -fallSpeed
            );
        }

        jumpCheck = IsGrounded();
    }

    private void FixedUpdate()
    {
        Move();

        // Apply jump request
        if (jumpRequested && IsGrounded())
        {
            rBody.linearVelocity = new Vector2(
                rBody.linearVelocity.x,
                jumpPow
            );

            jumpRequested = false;
        }

        // Apply jump cut
        if (jumpCutRequested)
        {
            if (rBody.linearVelocity.y > 0.0f)
            {
                rBody.linearVelocity = new Vector2(
                    rBody.linearVelocity.x,
                    rBody.linearVelocity.y * 0.5f
                );
            }

            jumpCutRequested = false;
        }
    }

    private void Move()
    {
        float targetSpeed = hori * speed;

        float speedDifference = targetSpeed - rBody.linearVelocity.x;

        float rate;

        if (Mathf.Abs(hori) > 0.01f)
        {
            rate = acceleration;
        }
        else
        {
            rate = deceleration;
        }

        float movement = speedDifference * rate;

        rBody.AddForce(Vector2.right * movement);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }
}
