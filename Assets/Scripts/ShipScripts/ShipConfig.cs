using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Config", menuName = "Ships/Ship Config")]
public class ShipConfig : ScriptableObject
{
    [Header("������ ��������������")]
    public string shipName = "SES14";
    public float baseMass = 300f; // ������ ���� ������� ��� �������� � ����
    public float maxFuel = 300f; // ������������ �ᒺ� ����
    public float maxCargo = 300f; // ������������ �ᒺ� ������� ��� ����

    [Header("�������")]
    public float thrust = 1800f; // ���� ��� �������
    public float fuelConsumption = 0.5f; // ������� �������� (�/�)
}
