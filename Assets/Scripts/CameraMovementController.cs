using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    public Transform target; // ��������� �� ��'���, �� ���� ���� ������
    public float smoothSpeed = 0.125f; // �������� ���� ������

    void LateUpdate()
    {
        Vector2 desiredPosition = target.position; // �������, �� ��� �� ������ ���������� ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // �������� ������� ��� ������
        smoothedPosition.z = -10;
        transform.position = smoothedPosition; // ����������� ���� ������� ������
    }
}
