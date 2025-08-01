using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Controls the behavior of a single star object, including its scale,
/// parallax movement, visibility, and destruction.
/// </summary>
public class StarController : MonoBehaviour
{
    /// <summary>
    /// Reference to the generator that spawned this star.
    /// Needed to update the star count when the star is destroyed.
    /// </summary>
    public StarGenerator starGenerator;

    /// <summary>
    /// Determines the parallax effect strength for this star.
    /// The closer to 0 — the slower the star moves.
    /// </summary>
    public float parallaxEffect;

    /// <summary>
    /// Reference to the spaceship to calculate relative velocity.
    /// </summary>
    public Transform spaceship;

    /// <summary>
    /// Initializes the star by scaling it based on parallax
    /// and randomly setting the animation duration.
    /// </summary>
    private void Start()
    {
        transform.localScale *= 0.35f;
        transform.localScale *= parallaxEffect;

        float[] durationMultiplicators = {0.75f, 1f, 1.25f};
        this.GetComponent<Animator>().SetFloat("Duration", durationMultiplicators[
            Random.Range(0, 3)]);
    }

    /// <summary>
    /// Updates star movement based on visibility and spaceship velocity.
    /// Destroys the star if it goes out of the camera view.
    /// </summary>
    void Update()
    {
        // Check if star is inside of camera field of view
        if (IsVisibleFromCamera())
        {
            GetComponent<Rigidbody2D>().velocity = 
                (1 - parallaxEffect) * spaceship.GetComponent<Rigidbody2D>().velocity;
        }
        else
        {
            // If star isn't inside - delete it
            starGenerator.starsCounter--;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method for determining whether a star is within the camera's field of view
    /// </summary>
    /// <returns></returns>
    bool IsVisibleFromCamera()
    {
        return GetComponent<Renderer>().isVisible;
    }
}
