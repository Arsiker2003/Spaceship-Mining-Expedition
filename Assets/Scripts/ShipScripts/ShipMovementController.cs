using UnityEngine;

/// <summary>
/// Controls the ship's movement and rotation using a single-engine model.
/// </summary>
public class ShipMovementController : MonoBehaviour
{
    [Header("Ship Engine Settings")]
    [Tooltip("Thrust force applied by one engine.")]
    public float acceleration = 20f;

    [Tooltip("Reference to the ship's Rigidbody2D component.")]
    public Rigidbody2D rb;

    private void Start()
    {
        // Offset the center of mass to simulate engine position for realistic rotation
        rb.centerOfMass = new Vector3(0, 1.5f);
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        ApplyThrust();
        ApplyRotation();
    }

    private float thrustInput;
    private float rotationInput;

    /// <summary>
    /// Reads user input each frame.
    /// </summary>
    private void HandleInput()
    {
        // Forward (W) = 1, Backward (S) = -0.5, None = 0
        if (Input.GetKey(KeyCode.W))
            thrustInput = 1f;
        else if (Input.GetKey(KeyCode.S))
            thrustInput = -0.5f;
        else
            thrustInput = 0f;

        // A/D keys or Left/Right arrows (mapped to Horizontal axis)
        rotationInput = -Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// Applies forward/backward thrust to the ship.
    /// </summary>
    private void ApplyThrust()
    {
        Vector2 force = thrustInput * acceleration * transform.up;
        rb.AddForce(force);
    }

    /// <summary>
    /// Applies torque to rotate the ship.
    /// </summary>
    private void ApplyRotation()
    {
        float torque = rotationInput * acceleration * 0.5f;
        rb.AddTorque(torque);
    }
}
