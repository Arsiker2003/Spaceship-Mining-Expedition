using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public enum ToolType
    {
        None,
        Drill,
        Gun,
        Scanner
    }

    private ToolType activeTool = ToolType.None;

    public MonoBehaviour[] drillScripts;
    public MonoBehaviour[] gunScripts;
    //public MonoBehaviour scanner;

    public Transform drills;
    public float drillsMoveDuration = 2f;

    void Start()
    {
        DeactivateAllTools();
    }
    void Update()
    {
        // Перевіряємо натискання клавіш для перемикання інструментів
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateTool(ToolType.Drill);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateTool(ToolType.Gun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateTool(ToolType.Scanner);
        }
    }

    void SetScriptsActive(MonoBehaviour[] scripts, bool isActive)
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = isActive;
        }
    }
    void DeactivateAllTools()
    {
        StartCoroutine(DeactivateDrills());
        SetScriptsActive(gunScripts, false);
        //scanner.enabled = false;
    }
    void ActivateTool(ToolType toolType)
    {
        // Деактивуємо всі інструменти
        DeactivateAllTools();

        // Активуємо вибраний інструмент
        switch (toolType)
        {
            case ToolType.Drill:
                StartCoroutine(ActivateDrills());
                break;
            case ToolType.Gun:
                SetScriptsActive(gunScripts, true);
                break;
            case ToolType.Scanner:
                //scanner.enabled = true;
                break;
        }

        // Оновлюємо активний інструмент
        activeTool = toolType;
    }

    public ToolType GetActiveTool()
    {
        return activeTool;
    }

    IEnumerator DeactivateDrills()
    {
        SetScriptsActive(drillScripts, false);
        yield return StartCoroutine(MoveDrills(0f)); ;
    }

    IEnumerator ActivateDrills()
    {
        yield return StartCoroutine(MoveDrills(3.75f));
        SetScriptsActive(drillScripts, true);
    }

    IEnumerator MoveDrills(float finalPosition)
    {
        float startPosition = drills.localPosition.y;
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            Vector3 newPosition = drills.localPosition;
            newPosition.y = Mathf.Lerp(startPosition, finalPosition, elapsedTime / drillsMoveDuration);
            drills.localPosition = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        

        
    }
}
