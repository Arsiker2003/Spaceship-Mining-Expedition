using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public float[] parallaxFactors;
    public GameObject[] starPrefabs; // Масив префабів зірок
    public int starsCounter = 0;
    public int maxStars = 100; // Максимальна кількість зірок
    public Transform spaceship;
    private Camera mainCamera; // Посилання на головну камеру

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

        Vector3 GetRandomPosition()
        {
            return new Vector3(
                    Random.Range(mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect,
                    mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect),
                    Random.Range(mainCamera.transform.position.y - mainCamera.orthographicSize,
                    mainCamera.transform.position.y + mainCamera.orthographicSize),
                    -9f);
        }
    }
}

