using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementController : MonoBehaviour
{
    public float acceleration = 20f;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.centerOfMass = new Vector3(0, 1.5f);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 accelerationVector = acceleration * transform.up;
            rb.AddForce(accelerationVector);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector2 accelerationVector = acceleration * -0.5f * transform.up;
            rb.AddForce(accelerationVector);
        }

        float rotationInput = -1 * Input.GetAxis("Horizontal"); // Зчитуємо ввід для повороту

        // Обертання корабля
        float rotationTorque = rotationInput * 50f;
        rb.AddTorque(rotationTorque);
    }
}
