using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarController : MonoBehaviour
{
    public StarGenerator starGenerator;
    public float parallaxEffect;
    public Transform spaceship;

     
    private void Start()
    {
        transform.localScale *= 0.35f;
        transform.localScale *= parallaxEffect;

        float[] durationMultiplicators = {0.75f, 1f, 1.25f};
        this.GetComponent<Animator>().SetFloat("Duration", durationMultiplicators[
            Random.Range(0, 2)]);
    }
    void Update()
    {
        // ����������, �� ���� ����������� � ��� �������� ������
        if (IsVisibleFromCamera())
        {
            GetComponent<Rigidbody2D>().velocity = 
                (1 - parallaxEffect) * spaceship.GetComponent<Rigidbody2D>().velocity;
        }
        else
        {
            // ���� ���� �� ������, �� ������ �������� ��
            starGenerator.starsCounter--;
            Destroy(gameObject);
        }
    }

    // ����� ��� ����������, �� ���� ����������� � ��� �������� ������
    bool IsVisibleFromCamera()
    {
        // ����������, �� ��'��� ������� � ����� ���� ������
        if (this.GetComponent<Renderer>().isVisible)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
