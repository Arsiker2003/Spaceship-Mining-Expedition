using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private float maxFuel = 300f; // Ліміт пального
    private float currentFuel; // Поточна кількість пального
    private float fuelDensity = 0.7f; // Щільність пального (умовно: 1 л = 0.7 кг)

    public event Action OnFuelChanged; // Івент для оновлення UI та маси корабля

    private void Start()
    {
        currentFuel = maxFuel; // Початкова заправка повного бака
    }

    public void SetMaxFuel(float maxFuel)
    {
        this.maxFuel = maxFuel;
        currentFuel = Mathf.Min(currentFuel, maxFuel); // Якщо пального більше ліміту – зменшуємо
        OnFuelChanged?.Invoke();
    }

    public float GetCurrentMass()
    {
        return currentFuel * fuelDensity; // Маса пального впливає на корабель
    }

    public void ConsumeFuel(float amount)
    {
        if (amount <= 0) return;
        currentFuel = Mathf.Max(currentFuel - amount, 0); // Запобігаємо від’ємним значенням
        OnFuelChanged?.Invoke();
    }

    public void Refuel(float amount)
    {
        if (amount <= 0) return;
        currentFuel = Mathf.Min(currentFuel + amount, maxFuel); // Не перевищуємо ліміт
        OnFuelChanged?.Invoke();
    }

    public float GetCurrentFuel()
    {
        return currentFuel; // Повертає скільки пального залишилося
    }
}

