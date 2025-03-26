using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private float maxFuel = 300f; // ˳�� ��������
    private float currentFuel; // ������� ������� ��������
    private float fuelDensity = 0.7f; // ٳ������ �������� (������: 1 � = 0.7 ��)

    public event Action OnFuelChanged; // ����� ��� ��������� UI �� ���� �������

    private void Start()
    {
        currentFuel = maxFuel; // ��������� �������� ������� ����
    }

    public void SetMaxFuel(float maxFuel)
    {
        this.maxFuel = maxFuel;
        currentFuel = Mathf.Min(currentFuel, maxFuel); // ���� �������� ����� ���� � ��������
        OnFuelChanged?.Invoke();
    }

    public float GetCurrentMass()
    {
        return currentFuel * fuelDensity; // ���� �������� ������ �� ��������
    }

    public void ConsumeFuel(float amount)
    {
        if (amount <= 0) return;
        currentFuel = Mathf.Max(currentFuel - amount, 0); // ��������� �䒺���� ���������
        OnFuelChanged?.Invoke();
    }

    public void Refuel(float amount)
    {
        if (amount <= 0) return;
        currentFuel = Mathf.Min(currentFuel + amount, maxFuel); // �� ���������� ���
        OnFuelChanged?.Invoke();
    }

    public float GetCurrentFuel()
    {
        return currentFuel; // ������� ������ �������� ����������
    }
}

