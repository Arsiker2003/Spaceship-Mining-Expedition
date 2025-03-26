using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipMovement : MonoBehaviour
{
    public FuelTank fuelTank;
    protected float thrust;
    protected float fuelConsumption;
    public virtual void SetFuelTank(FuelTank tank)
    {
        fuelTank = tank;
    }
    public void SetThrust(float newThrust)
    {
        thrust = newThrust;
    }

    public void SetFuelConsumption(float newFuelConsumption)
    {
        fuelConsumption = newFuelConsumption;
    }
    public abstract void HandleMovement();
}
