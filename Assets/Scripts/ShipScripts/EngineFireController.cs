using UnityEngine;

/// <summary>
/// Controls the engine fire visual effect by changing Animator parameters
/// based on player input.
/// </summary>
public class EngineFireController : MonoBehaviour
{
    public Animator leftRotationEngine;
    public Animator leftForwardEngine;
    public Animator leftBackwardEngine;
    public Animator rightRotationEngine;
    public Animator rightForwardEngine;
    public Animator rightBackwardEngine;

    void Update()
    {
        // Forward engines (W)
        bool isForward = Input.GetKey(KeyCode.W);
        leftForwardEngine.SetInteger("FireOn", isForward ? 1 : 2);
        rightForwardEngine.SetInteger("FireOn", isForward ? 1 : 2);

        // Backward engines (S)
        bool isBackward = Input.GetKey(KeyCode.S);
        leftBackwardEngine.SetInteger("FireOn", isBackward ? 1 : 2);
        rightBackwardEngine.SetInteger("FireOn", isBackward ? 1 : 2);

        // Rotation (A/D control opposite engines)
        leftRotationEngine.SetInteger("FireOn", Input.GetKey(KeyCode.A) ? 1 : 2);
        rightRotationEngine.SetInteger("FireOn", Input.GetKey(KeyCode.D) ? 1 : 2);
    }
}
