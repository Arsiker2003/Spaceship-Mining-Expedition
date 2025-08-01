using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that handles tool switching, 
/// needed because only one tool should work simultaneously
/// </summary>
public class ToolManager : MonoBehaviour
{
    /// <summary>
    /// Enumaration for all existing tools of the ship
    /// </summary>
    public enum ToolType
    {
        None,
        Drill,
        Gun,
        Scanner
    }
    /// <summary>
    /// Array of all scripts that connected to drills
    /// </summary>
    public MonoBehaviour[] drillScripts;
    /// <summary>
    /// Array of all scripts that connected to side cannons
    /// </summary>
    public MonoBehaviour[] gunScripts;
    //public MonoBehaviour scanner;
    /// <summary>
    /// Reference to the transform component of drills, 'cause they will move 
    /// </summary>
    public Transform drills;
    /// <summary>
    /// Time in seconds spent for drills to move
    /// </summary>
    public float drillsMoveDuration = 2f;

    private ToolType activeTool = ToolType.None;
    void Start()
    {
        DeactivateAllTools();
    }
    void Update()
    {
        // Switch instruments according to pressed number key
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
    /// <summary>
    /// Activates or disables all scripts in array
    /// </summary>
    /// <param name="scripts">Array of scripts to enable or disable</param>
    /// <param name="isActive">Activation or disabling param</param>
    void SetScriptsActive(MonoBehaviour[] scripts, bool isActive)
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = isActive;
        }
    }
    /// <summary>
    /// Deactivates all tools of ship
    /// </summary>
    void DeactivateAllTools()
    {
        StartCoroutine(DeactivateDrills());
        SetScriptsActive(gunScripts, false);
        //scanner.enabled = false;
    }
    /// <summary>
    /// Activates one tool while all others will be deactivated
    /// </summary>
    /// <param name="toolType">Instrument name from enum</param>
    void ActivateTool(ToolType toolType)
    {
        DeactivateAllTools();
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
        activeTool = toolType;
    }
    /// <summary>
    /// Return name of tool currently working
    /// </summary>
    /// <returns>Name of active tool</returns>
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
    /// <summary>
    /// Move the drilling tool relatively to center of the ship
    /// </summary>
    /// <param name="finalPosition">Desired vertical coordinate</param>
    /// <returns></returns>
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
