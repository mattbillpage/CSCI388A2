using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    public Light incomingLight;
    public Light reflectedLight;

    [Range(0f, 1f)]
    public float reflectThreshold = 0.2f; 
    // lower = more forgiving, higher = stricter cutoff

    private Renderer mirrorRenderer;

    void Start()
    {
        mirrorRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!incomingLight || !reflectedLight || !mirrorRenderer)
            return;

        // Direction FROM light toward mirror
        Vector3 incomingDir = (transform.position - incomingLight.transform.position).normalized;

        // Mirror normal (flip if needed)
        Vector3 normal = -transform.forward;

        // ensures that the mirror only reflects light when the reflective surface is being hit
        float alignment = Vector3.Dot(normal, incomingDir);
        if (alignment < reflectThreshold)
        {
            // Too angled → turn off reflection
            reflectedLight.enabled = false;
            return;
        }

        reflectedLight.enabled = true;

        // Reflect direction
        Vector3 reflectedDir = Vector3.Reflect(incomingDir, normal);

        // Use actual visual centre of mirror
        Vector3 mirrorCentre = mirrorRenderer.bounds.center;

        reflectedLight.transform.position = mirrorCentre;
        reflectedLight.transform.rotation = Quaternion.LookRotation(reflectedDir);
    }
}