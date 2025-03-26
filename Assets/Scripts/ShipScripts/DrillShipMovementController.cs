using UnityEngine;

public class DrillShipMovementController : ShipMovement
{
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector3(0, 1.5f);
    }

    public override void HandleMovement()
    {
        if (fuelTank == null) return; // Перевіряємо, чи взагалі є бак
        if (Input.GetKey(KeyCode.W) && fuelTank.GetCurrentFuel() > 0) // Прискорення
        {
            Vector2 accelerationVector = thrust * transform.up;
            rb.AddForce(accelerationVector);
            fuelTank.ConsumeFuel(fuelConsumption * Time.deltaTime); //  Два двигуни, 100% тяги
        }
        else if (Input.GetKey(KeyCode.S) && fuelTank.GetCurrentFuel() > 0) // Гальмування
        {
            Vector2 accelerationVector = thrust * -0.5f * transform.up;
            rb.AddForce(accelerationVector);
            fuelTank.ConsumeFuel(fuelConsumption * Time.deltaTime); //  Два двигуни, але 50% ефективності
        }

        float rotationInput = -Input.GetAxis("Horizontal"); // Поворот

        if (rotationInput != 0 && fuelTank.GetCurrentFuel() > 0)
        {
            float rotationTorque = rotationInput * 0.5f * thrust;
            rb.AddTorque(rotationTorque);
            fuelTank.ConsumeFuel((fuelConsumption / 2) * Time.deltaTime); //  Один двигун, 100% тяги
        }

    }
}
