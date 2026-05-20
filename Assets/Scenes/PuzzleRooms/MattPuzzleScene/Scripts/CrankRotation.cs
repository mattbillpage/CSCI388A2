using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CrankRotation : MonoBehaviour
{
    public Transform crankPivot;
    public Transform drivenObject;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private Transform interactorTransform;

    private float lastAngle;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        interactorTransform = args.interactorObject.transform;
        lastAngle = GetAngle();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        interactorTransform = null;
    }

    void Update()
    {
        if (interactorTransform == null) return;

        float currentAngle = GetAngle();
        float delta = Mathf.DeltaAngle(lastAngle, currentAngle);

        crankPivot.Rotate(Vector3.forward, delta, Space.Self);

        lastAngle = currentAngle;

        UpdateDrivenObject();
    }

    float GetAngle()
    {
        Vector3 dir = interactorTransform.position - crankPivot.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    void UpdateDrivenObject()
    {
        float normalized = crankPivot.localEulerAngles.z / 360f;

        if (drivenObject != null)
        {
            drivenObject.localRotation = Quaternion.Euler(0, normalized * 360f, 0);
        }
    }
}