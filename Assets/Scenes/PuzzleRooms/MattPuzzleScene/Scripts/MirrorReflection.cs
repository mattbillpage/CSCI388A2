using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    public Light incomingLight;
    public Light reflectedLight;

    [Range(0f, 1f)]
    public float reflectThreshold = 0.2f;

    public float maxDistance = 50f;

    private Renderer mirrorRenderer;

    void Start()
    {
        mirrorRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!incomingLight || !reflectedLight || !mirrorRenderer)
            return;

        reflectedLight.enabled = false; // ensure light is not on when it shouldnt b

        Vector3 mirrorCentre = mirrorRenderer.bounds.center;

        Vector3 toMirror =
            mirrorCentre - incomingLight.transform.position;

        float distance = toMirror.magnitude;
        Vector3 direction = toMirror / distance;

        float forwardCheck =
            Vector3.Dot(incomingLight.transform.forward, direction);

        if (forwardCheck <= 0f)
            return;

        float spotAngle =
            incomingLight.spotAngle * 0.5f;

        float angle =
            Vector3.Angle(incomingLight.transform.forward, direction);

        if (angle > spotAngle)
            return;

        Ray ray = new Ray(incomingLight.transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.GetComponent<MirrorReflection>() == null)
                return;
        }
        else
        {
            return;
        }

        // Mirror normal
        Vector3 normal = -transform.forward;

        float alignment =
            Vector3.Dot(normal, direction);

        if (alignment < reflectThreshold)
            return;

        // Reflect
        Vector3 reflectedDir =
            Vector3.Reflect(direction, normal);

        reflectedLight.enabled = true;
        reflectedLight.transform.position = mirrorCentre;
        reflectedLight.transform.rotation =
            Quaternion.LookRotation(reflectedDir);
    }
}