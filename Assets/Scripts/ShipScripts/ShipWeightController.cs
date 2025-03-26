using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeightController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FuelTank fuelTank;
    [SerializeField] private CargoBay cargoBay;

    private float baseMass;
    private float lastUpdatedMass;
    private float lastMassChangeTime;

    private bool massNeedsUpdate = false;

    private void Start()
    {
        baseMass = rb.mass; // Початкова маса без вантажу і пального
        UpdateMass();
    }

    private void NotifyMassChange()
    {
        float newMass = baseMass + fuelTank.GetCurrentMass() + cargoBay.GetCurrentMass();

        if (Mathf.Abs(newMass - lastUpdatedMass) > 0.01f * baseMass) // Оновлюємо, якщо зміна значна (1%)
        {
            UpdateMass();
        }
        else
        {
            lastMassChangeTime = Time.time;
            massNeedsUpdate = true;
        }
    }

    private void FixedUpdate()
    {
        if (massNeedsUpdate && Time.time - lastMassChangeTime >= 1f)
        {
            UpdateMass();
            massNeedsUpdate = false;
        }
    }

    private void UpdateMass()
    {
        float fuelMass = fuelTank != null ? fuelTank.GetCurrentMass() : 0f;
        float cargoMass = cargoBay != null ? cargoBay.GetCurrentMass() : 0f;

        rb.mass = baseMass + fuelMass + cargoMass;
        lastUpdatedMass = rb.mass;

        Debug.Log("Масу корабля оновлено: " + rb.mass); // Лог для дебагу
    }

    public void SetFuelTank(FuelTank tank)
    {
        fuelTank = tank;
        if (fuelTank != null)
            fuelTank.OnFuelChanged += NotifyMassChange;
    }

    public void SetCargoBay(CargoBay bay)
    {
        cargoBay = bay;
        if (cargoBay != null)
            cargoBay.OnCargoChanged += NotifyMassChange;
    }


    private void OnDestroy()
    {
        if (fuelTank != null) fuelTank.OnFuelChanged -= NotifyMassChange;
        if (cargoBay != null) cargoBay.OnCargoChanged -= NotifyMassChange;
    }
}

