using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipConfig config;

    [SerializeField] private FuelTank fuelTank;
    [SerializeField] private CargoBay cargoBay;
    [SerializeField] private ShipMovement movementController;
    [SerializeField] private ShipWeightController weightController;
    private Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // якщо не задано вручну Ц шукаЇмо автоматично
        if (fuelTank == null)
            fuelTank = GetComponentInChildren<FuelTank>();
        if (cargoBay == null)
            cargoBay = GetComponentInChildren<CargoBay>();
        if (movementController == null)
            movementController = GetComponent<ShipMovement>();
        if (weightController == null)
            weightController = GetComponent<ShipWeightController>();

        // якщо не знайдено в доч≥рн≥х компонентах - створити пуст≥ об'Їкти ≥
        // додати потр≥бн≥ компоненти на них
        if (fuelTank == null)
        {
            GameObject fuelObject = new GameObject("FuelTank");
            fuelObject.transform.parent = this.transform;
            fuelTank = fuelObject.AddComponent<FuelTank>();
        }
        if (cargoBay == null)
        {
            GameObject cargoObject = new GameObject("CargoBay");
            cargoObject.transform.parent = this.transform;
            cargoBay = cargoObject.AddComponent<CargoBay>();
        }
        if (weightController == null)
        {
            weightController = gameObject.AddComponent<ShipWeightController>();
        }
        if (movementController == null)
        {
            movementController = gameObject.AddComponent<ShipMovement>();
        }

        if (config != null)
        {
            rb.mass = config.baseMass;
            if (fuelTank != null) fuelTank.SetMaxFuel(config.maxFuel);
            if (cargoBay != null) cargoBay.SetMaxCargo(config.maxCargo);
            if (movementController != null)
            {
                movementController.SetThrust(config.thrust);
                movementController.SetFuelConsumption(config.fuelConsumption);
                movementController.SetFuelTank(fuelTank);
            }
            if (weightController != null)
            {
                weightController.SetFuelTank(fuelTank);
                weightController.SetCargoBay(cargoBay);
            }
        }
        else
        {
            Debug.LogError("ShipConfig не призначений дл€ " + gameObject.name);
        }
    }
}
