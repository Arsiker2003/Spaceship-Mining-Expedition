using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    public Transform target; // Посилання на об'єкт, за яким слідує камера
    public float smoothSpeed = 0.125f; // Плавність руху камери

    void LateUpdate()
    {
        Vector2 desiredPosition = target.position; // Позиція, до якої ми хочемо перемістити камеру
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Виконуємо плавний рух камери
        smoothedPosition.z = -10;
        transform.position = smoothedPosition; // Застосовуємо нову позицію камери
    }
}
