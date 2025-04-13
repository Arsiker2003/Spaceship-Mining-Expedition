using UnityEngine;
using UnityEngine.U2D;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] int minPoints = 6;
    [SerializeField] int maxPoints = 10;
    [SerializeField] float minRadius = 10f;
    [SerializeField] float maxRadius = 30f;
    public GameObject asteroidPrefab; // ������ ��������

    public void Start()
    {
        // �������� ��'���� �������� ���� ������� 15 ������� ������� ��'����
        for (int i = 0; i < 10; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 15f; // ��������� ������� � ����� 15
            Vector2 spawnPosition = (Vector2)transform.position + offset;
            GenerateAsteroid(spawnPosition);
        }
    }
    public void GenerateAsteroid(Vector2 position)
    {
        // ��������� ���� �������
        GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);

        // �������� SpriteShapeController
        SpriteShapeController shapeController = asteroid.GetComponent<SpriteShapeController>();

        // �������� PolygonCollider2D
        PolygonCollider2D polyCollider = asteroid.GetComponent<PolygonCollider2D>();

        // �������� ��������� ����� ��������
        GenerateShape(shapeController);

        // �������� ��������
        GenerateCollider(shapeController, polyCollider);
    }

    private void GenerateShape(SpriteShapeController shapeController)
    {
        Spline spline = shapeController.spline;
        int pointCount = Random.Range(minPoints, maxPoints);

        spline.Clear();

        for (int i = 0; i < pointCount; i++)
        {
            float angle = i * Mathf.Deg2Rad * (360f / pointCount);
            float radius = Random.Range(minRadius, maxRadius);
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            spline.InsertPointAt(i, point);
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            Vector2 tangent = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle)) * radius * 0.4f; // 0.4f � ������� ����������

            spline.SetLeftTangent(i, -tangent);
            spline.SetRightTangent(i, tangent);
        }

        shapeController.BakeMesh();
    }

    private void GenerateCollider(SpriteShapeController shapeController, PolygonCollider2D polyCollider)
    {
        Spline spline = shapeController.spline;
        Vector2[] colliderPoints = new Vector2[spline.GetPointCount()];

        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            colliderPoints[i] = (Vector2)spline.GetPosition(i);
        }

        polyCollider.SetPath(0, colliderPoints);
    }

    private void AssignOre(GameObject asteroid)
    {
        throw new System.NotImplementedException();
    }
}
