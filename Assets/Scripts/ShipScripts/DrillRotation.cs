using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that literally creates visual "rotation" of drills
/// </summary>
public class DrillRotation : MonoBehaviour
{
    /// <summary>
    /// Rotation speed of the drill texture
    /// </summary>
    public float rotationSpeed = 1.0f;
    private Material material;
    private Vector2 offset;

    void Start()
    {
        // Отримати матеріал з Sprite Renderer
        material = GetComponent<SpriteRenderer>().material;
        offset = material.mainTextureOffset;
    }

    void Update()
    {
        if (this.gameObject.name == "LeftDrill")
        {
            offset.x -= Time.deltaTime * rotationSpeed;
            if (offset.x < -0.2) offset.x = 0.2f;
        }
        if (this.gameObject.name == "RightDrill")
        {
            offset.x += Time.deltaTime * rotationSpeed;
            if (offset.x > 0.2) offset.x = -0.2f;
        }

        material.mainTextureOffset = offset;
    }
}
