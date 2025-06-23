using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class AsteroidDetailManager : MonoBehaviour
{
    public List<Sprite> detailSprites; // Кратери, тріщини, виступи
    public int minDetails = 2;
    public int maxDetails = 6;

    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minDistanceBetweenDetails = 1.5f;


    private void Awake()
    {
        detailSprites = new List<Sprite>();

        if (detailSprites.Count == 0)
        {
            Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Sprites/Asteroids/AsteroidDetails");
            detailSprites.AddRange(loadedSprites);
        }
    }
    public void AddDetailsToAsteroid()
    {
        int detailCount = Random.Range(minDetails, this.maxDetails + 1);

        
        // 1. Отримати сплайн
        Spline spline = GetComponent<SpriteShapeController>().spline;
        int pointCount = spline.GetPointCount();

        // 2. Обмежити кількість деталей (наприклад: максимум половина кількості точок)
        int maxDetails = Mathf.Min(pointCount / 2, detailSprites.Count);

        List<Vector2> usedPositions = new List<Vector2>();
        for (int i = 0; i < maxDetails; i++)
        { 
            // --- 3. Вибираємо випадкову точку на сплайні ---
            int index = Random.Range(0, pointCount);
            Vector2 point = spline.GetPosition(index);
            Vector2 toCenter = (Vector2)transform.position - point;
            Vector2 inwardDir = toCenter.normalized;
            

            // 6. Зміщуємо точку вглиб форми
            float offset = Random.Range(3f + minScale, 2 * maxScale); // "глибина" деталі

            
            int attempts = 0;
            int placed = 0;
            while (placed < detailCount && attempts < 20)
            {
                Vector2 candidate = point + inwardDir * offset;

                if (!IsTooClose(candidate, usedPositions, minDistanceBetweenDetails + offset))
                {
                    usedPositions.Add(candidate);
                    GameObject detail = new GameObject($"SmartDetail_{placed}");
                    detail.transform.parent = this.transform;
                    detail.transform.localPosition = candidate;
                    SpriteRenderer sr = detail.AddComponent<SpriteRenderer>();
                    sr.sprite = detailSprites[Random.Range(0, detailSprites.Count)];
                    sr.sortingOrder = 1;

                    float rotation = Random.Range(0f, 360f);
                    float scale = Random.Range(minScale, maxScale);

                    detail.transform.localRotation = Quaternion.Euler(0, 0, rotation);
                    detail.transform.localScale = Vector3.one * scale;
                    placed++;
                }
                attempts++;
            }

            
        }
    }
    bool IsTooClose(Vector2 newPos, List<Vector2> existing, float minDistance)
    {
        foreach (var pos in existing)
        {
            if (Vector2.Distance(newPos, pos) < minDistance)
                return true;
        }
        return false;
    }

}