using UnityEngine;
using UnityEngine.U2D;
/// <summary>
/// Class that creates big asteroid containing ore to mine
/// </summary>
public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] int minPoints = 6;
    [SerializeField] int maxPoints = 10;
    [SerializeField] float minRadius = 10f;
    [SerializeField] float maxRadius = 30f;
    /// <summary>
    /// Reference to asteroid prefab
    /// </summary>
    public GameObject asteroidPrefab; 

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

        // �������� ����� �� ������� (������, �������)
        asteroid.GetComponent<AsteroidDetailManager>().AddDetailsToAsteroid();
    }
    /// <summary>
    /// Generates random sprite shape to make each asteroid look different
    /// </summary>
    /// <param name="shapeController">Reference to newly created shape</param>
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
            Vector2 tangent = 0.4f * radius * new Vector2(-Mathf.Sin(angle),
                Mathf.Cos(angle)); // 0.4f � ������� ����������

            spline.SetLeftTangent(i, -tangent);
            spline.SetRightTangent(i, tangent);
        }

        shapeController.BakeMesh();
    }
    /// <summary>
    /// Adds collider component to asteroid that matches it's form
    /// </summary>
    /// <param name="shapeController">Reference to sprite shape controller of asteroid</param>
    /// <param name="polyCollider">Reference to collider component of asteroid</param>
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
