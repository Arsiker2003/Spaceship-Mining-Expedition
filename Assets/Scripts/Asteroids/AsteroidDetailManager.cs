using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;
/// <summary>
/// Class that adds details on asteroid
/// </summary>
public class AsteroidDetailManager : MonoBehaviour
{
    /// <summary>
    /// List of sprites of all possible details
    /// </summary>
    public List<Sprite> detailSprites;
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
    /// <summary>
    /// Create details inside of the asteroid's sprite shape and withount intersections
    /// </summary>
    public void AddDetailsToAsteroid()
    {
        int detailCount = Random.Range(minDetails, this.maxDetails + 1);
        Spline spline = GetComponent<SpriteShapeController>().spline;
        int pointCount = spline.GetPointCount();
        // Add limit to amount of details
        int maxDetails = Mathf.Min(pointCount / 2, detailSprites.Count);
        // List of used positions to exclude possibility that details will spawn
        // in same places
        List<Vector2> usedPositions = new List<Vector2>();
        for (int i = 0; i < maxDetails; i++)
        { 
            int index = Random.Range(0, pointCount);
            Vector2 point = spline.GetPosition(index);
            Vector2 toCenter = (Vector2)transform.position - point;
            Vector2 inwardDir = toCenter.normalized;
            // Move detail slightly "inside" of asteroid, to make every detail
            // looks like it is on the asteroid. This also eliminates
            // possibility that detail could be beyond asteroid sprite
            float offset = Random.Range(3f + minScale, 2 * maxScale); 

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
    /// <summary>
    /// Define if position for new detail is far enough from other details
    /// </summary>
    /// <param name="newPos">Position for new detail</param>
    /// <param name="existing">List of used positions</param>
    /// <param name="minDistance">Minimum distance between details</param>
    /// <returns>Returns "true" when distance between details is bigger than minimum</returns>
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