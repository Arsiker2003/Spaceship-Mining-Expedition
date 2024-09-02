using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineFireController : MonoBehaviour
{
    public Animator leftRotationEngine;
    public Animator leftForwardEngine;
    public Animator leftBackwardEngine;
    public Animator rightRotationEngine;
    public Animator rightForwardEngine;
    public Animator rightBackwardEngine;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            leftForwardEngine.SetInteger("FireOn", 1);
            rightForwardEngine.SetInteger("FireOn", 1);
        }
        else        
        {
            leftForwardEngine.SetInteger("FireOn", 2);
            rightForwardEngine.SetInteger("FireOn", 2);
        }

        if (Input.GetKey(KeyCode.S))
        {
            leftBackwardEngine.SetInteger("FireOn", 1);
            rightBackwardEngine.SetInteger("FireOn", 1);
        }
        else
        {
            leftBackwardEngine.SetInteger("FireOn", 2);
            rightBackwardEngine.SetInteger("FireOn", 2);
        }

        if (Input.GetKey(KeyCode.A))
        {
            leftRotationEngine.SetInteger("FireOn", 1);
        }
        else
        {
            leftRotationEngine.SetInteger("FireOn", 2);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rightRotationEngine.SetInteger("FireOn", 1);
        }
        else
        {
            rightRotationEngine.SetInteger("FireOn", 2);
        }
    }
}
