using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillRotation : MonoBehaviour
{
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
        // Анімація offset текстури
        
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
