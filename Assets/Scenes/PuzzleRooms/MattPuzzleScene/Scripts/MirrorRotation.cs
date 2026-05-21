using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MirrorRotation : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(FreezeOnGrab);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(FreezeOnGrab);
    }

    private void FreezeOnGrab(SelectEnterEventArgs args)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePosition |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
        // supposed to stop rotation and make it only on the y axis, dont know why its not working at the moment but ill come back to this; its at the very least seems to be slowing down the rotation on the other axis so its fine for now
    }
}