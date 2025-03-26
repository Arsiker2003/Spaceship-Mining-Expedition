using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Config", menuName = "Ships/Ship Config")]
public class ShipConfig : ScriptableObject
{
    [Header("Основні характеристики")]
    public string shipName = "SES14";
    public float baseMass = 300f; // Базова маса корабля без пального і руди
    public float maxFuel = 300f; // Максимальний об’єм бака
    public float maxCargo = 300f; // Максимальний об’єм бункера для руди

    [Header("Двигуни")]
    public float thrust = 1800f; // Тяга всіх двигунів
    public float fuelConsumption = 0.5f; // Витрата пального (т/с)
}
