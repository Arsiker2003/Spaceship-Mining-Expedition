using System;
using System.Collections.Generic;
using UnityEngine;

public class CargoBay : MonoBehaviour
{
    [SerializeField] private float maxCargo = 300f; // ����������� �������
    private float currentCargo = 0f; // ������� ���� �������
    private Dictionary<ResourceType, float> cargoContents = new Dictionary<ResourceType, float>(); // ���� �������

    public event Action OnCargoChanged; // ����� ��� ����������� ����� ������

    public void SetMaxCargo(float maxCargo)
    {
        this.maxCargo = maxCargo;
        currentCargo = Mathf.Min(currentCargo, maxCargo); // ��������� ��������������
        OnCargoChanged?.Invoke();
    }

    public float GetCurrentMass()
    {
        return currentCargo; // ���� ������� ��� �������
    }

    public void AddCargo(ResourceType type, float amount)
    {
        if (amount <= 0) return;

        if (currentCargo + amount > maxCargo)
        {
            amount = maxCargo - currentCargo; // ���� �� ������� ��� - ������ ����� �������� ����
        }

        if (!cargoContents.ContainsKey(type))
        {
            cargoContents[type] = 0f;
        }

        cargoContents[type] += amount;
        currentCargo += amount;
        OnCargoChanged?.Invoke();
    }

    public void RemoveCargo(ResourceType type, float amount)
    {
        if (amount <= 0 || !cargoContents.ContainsKey(type)) return;

        float removedAmount = Mathf.Min(amount, cargoContents[type]);
        cargoContents[type] -= removedAmount;
        currentCargo -= removedAmount;

        if (cargoContents[type] <= 0)
        {
            cargoContents.Remove(type);
        }

        OnCargoChanged?.Invoke();
    }
    public void ClearCargo()
    {
        cargoContents.Clear();
        currentCargo = 0f;
        OnCargoChanged?.Invoke();
    }
    public Dictionary<ResourceType, float> GetCurrentCargo()
    {
        return new Dictionary<ResourceType, float>(cargoContents); // ������� ����, ��� ������ ������� �� ����� ������� ������
    }
    public void DumpCargo(ResourceType type, float amount)
    {
        if (amount <= 0 || !cargoContents.ContainsKey(type)) return;

        float removedAmount = Mathf.Min(amount, cargoContents[type]);
        cargoContents[type] -= removedAmount;
        currentCargo -= removedAmount;

        if (cargoContents[type] <= 0)
        {
            cargoContents.Remove(type);
        }

        OnCargoChanged?.Invoke();
        Debug.Log($"������� {removedAmount} ���� {type}");
    }
}
