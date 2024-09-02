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

        // Визначимо напрямок до курсора в локальних координатах відносно центру обертання
        Vector3 direction = mousePosition - rotationCenter.position;

        // Знайдемо кут між напрямком до курсора і поточним напрямком пушки
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Отримаємо поточний кут обертання пушки
        float currentAngle = transform.eulerAngles.z;

        // Обмежимо обертання до осі Z з урахуванням швидкості обертання
        float clampedAngle = Mathf.MoveTowardsAngle(currentAngle, angle, rotationSpeed * Time.deltaTime);

        // Вирахуємо точку обертання навколо центру обертання
        Vector3 rotatePoint = rotationCenter.position;

        // Обертаємо пушку навколо центру обертання з обмеженою швидкістю
        transform.RotateAround(rotatePoint, Vector3.forward, Mathf.DeltaAngle(currentAngle, clampedAngle));
    }
}
