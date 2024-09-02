using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float rotationSpeed = 5f;

    public Transform rotationCenter;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ��������� �������� �� ������� � ��������� ����������� ������� ������ ���������
        Vector3 direction = mousePosition - rotationCenter.position;

        // �������� ��� �� ��������� �� ������� � �������� ��������� �����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // �������� �������� ��� ��������� �����
        float currentAngle = transform.eulerAngles.z;

        // �������� ��������� �� �� Z � ����������� �������� ���������
        float clampedAngle = Mathf.MoveTowardsAngle(currentAngle, angle, rotationSpeed * Time.deltaTime);

        // �������� ����� ��������� ������� ������ ���������
        Vector3 rotatePoint = rotationCenter.position;

        // �������� ����� ������� ������ ��������� � ��������� ��������
        transform.RotateAround(rotatePoint, Vector3.forward, Mathf.DeltaAngle(currentAngle, clampedAngle));
    }
}
