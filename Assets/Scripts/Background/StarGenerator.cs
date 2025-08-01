using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that creates stars on the background and adds to them logical components
/// </summary>
public class StarGenerator : MonoBehaviour
{
    /// <summary>
    /// Array of possible strengths of parallax effect
    /// </summary>
    public float[] parallaxFactors;
    /// <summary>
    /// Array of prefabs that will be used to create star objects
    /// </summary>
    public GameObject[] starPrefabs;
    /// <summary>
    /// Max quantity of stars that can exist altogether
    /// </summary>
    public int maxStars = 100; 
    /// <summary>
    /// Reference to transform component of the ship, 
    /// needed for further effects
    /// </summary>
    public Transform spaceship;
    private Camera mainCamera; // Link to camera
    /// <summary>
    /// Simple variable for counting stars, needs to remain public
    /// cause: created stars delete themselves and this decreases the counter
    /// </summary>
    public int starsCounter = 0; 

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (starsCounter <= maxStars)
        {
            GenerateStar();
        }
    }

    /// <summary>
    /// Generate star prefab within main camera's field of view
    /// </summary>
    void GenerateStar()
    {
        GameObject star = Instantiate(starPrefabs[Random.Range(0, starPrefabs.Length)],
            GetRandomPosition(),
            Quaternion.identity);
        star.AddComponent<StarController>().starGenerator = this;
        star.AddComponent<Rigidbody2D>().gravityScale = 0;

        star.GetComponent<StarController>().parallaxEffect = parallaxFactors[Random.Range(0, parallaxFactors.Length)];
        star.GetComponent<StarController>().spaceship = this.spaceship;

        starsCounter++;
    }

    /// <summary>
    /// Generate random position within main camera's field of view
    /// </summary>
    /// <returns>Coordinates for star as Vector3 object</returns>
    Vector3 GetRandomPosition()
    {
        return new Vector3(
                Random.Range(mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect,
                mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect),
                Random.Range(mainCamera.transform.position.y - mainCamera.orthographicSize,
                mainCamera.transform.position.y + mainCamera.orthographicSize),
                -9f); // -9f for z coordinate to nivelate troubles of rendering 

    }
}

