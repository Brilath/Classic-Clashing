using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)]
    private float maxSpeed = 100f;
    [SerializeField, Range(0f, 10f)]
    private float rotationSpeed = 1f;

    [SerializeField, Range(0f, 100f)]
    private float maxAcceleration = 100f;
    [SerializeField, Range(0f, 100f)]
    private float maxAirAcceleration = 100f;

    [SerializeField, Range(0f, 1f)]
    private float bounciness = 0.5f;

    private Rigidbody body;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 desiredVelocity;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool desiredJump;
    [SerializeField] private bool onGround;
    [SerializeField, Range(0f, 10f)]
    private float jumpHeight = 2f;
    [SerializeField, Range(0, 5)]
    private int maxAirJumps = 0;
    [SerializeField] private int jumpPhase;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 movementHorizontal = transform.right * playerInput.x;
        Vector3 movementVertical = transform.forward * playerInput.y;
        desiredVelocity = (movementHorizontal + movementVertical).normalized * maxSpeed;

        // Jump
        desiredJump |= Input.GetButtonDown("Jump");

        // Rotate
        float playerRotation = Input.GetAxis("Mouse X");
        Vector3 rotationVector = new Vector3(0, playerRotation, 0) * rotationSpeed;
        Rotate(rotationVector);
    }

    private void FixedUpdate()
    {
        UpdateState();

        // Depending if grounded use the correct acceleration
        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        body.MoveRotation(body.rotation * Quaternion.Euler(rotation));
        body.velocity = velocity;
        onGround = false;
    }

    private void UpdateState()
    {
        velocity = body.velocity;
        if (onGround)
        {
            jumpPhase = 0;
        }
    }

    private void Rotate(Vector3 dir)
    {
        rotation = dir;
    }

    private void RotateCamera(float cameraRotation)
    {

    }

    private void Jump()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            // Limit Upward Velocity
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }

    // Check if collision is with the ground
    private void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

}