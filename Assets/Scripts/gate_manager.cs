// 🚪 GateManager.cs
// Handles gate open/close logic, escape check, and gate timing

using UnityEngine;

public class GateManager : MonoBehaviour
{
    // public Animator gateAnimator;
    public Collider gateCollider;

    // [Header("Gate Timings")]
    // public float openDuration = 10f;
    // public float delayBeforeOpen = 20f;

    // private bool isGateOpen = false;

    // void Start()
    // {
    //     Invoke(nameof(OpenGate), delayBeforeOpen);
    // }

    // public void OpenGate()
    // {
    //     if (isGateOpen) return;

    //     Debug.Log("Gate opening!");
    //     isGateOpen = true;
    //     gateAnimator.SetTrigger("Open");
    //     gateCollider.enabled = false;

    //     Invoke(nameof(CloseGate), openDuration);
    // }

    // public void CloseGate()
    // {
    //     Debug.Log("Gate closing!");
    //     isGateOpen = false;
    //     gateAnimator.SetTrigger("Close");
    //     gateCollider.enabled = true;
    // }

    private void OnTriggerEnter(Collider other)
    {
        // if (!isGateOpen) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player escaped through the gate!");
            GameManager.Instance.PlayerEscaped();
        }
    }

    // public bool IsGateOpen()
    // {
    //     return isGateOpen;
    // }
}
