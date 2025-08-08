using UnityEngine;

/// <summary>
/// Visually simulates drill rotation by offsetting the texture.
/// This creates an illusion of spinning without rotating the object.
/// </summary>
public class DrillRotation : MonoBehaviour
{
    /// <summary>Speed of texture offset, controlling visual spin speed.</summary>
    public float rotationSpeed = 1.0f;

    private Material material;
    private Vector2 offset;

    private void Start()
    {
        // Get the material from the sprite (assumes material uses a shader that supports offset)
        material = GetComponent<SpriteRenderer>().material;
        offset = material.mainTextureOffset;
    }

    private void Update()
    {
        // Determine direction of fake rotation based on object name
        if (gameObject.name == "LeftDrill")
        {
            offset.x -= Time.deltaTime * rotationSpeed;
            if (offset.x < -0.2f) offset.x = 0.2f; // Reset loop
        }
        else if (gameObject.name == "RightDrill")
        {
            offset.x += Time.deltaTime * rotationSpeed;
            if (offset.x > 0.2f) offset.x = -0.2f; // Reset loop
        }

        material.mainTextureOffset = offset;
    }
}
