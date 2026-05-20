using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    public Light incomingLight;
    public Light reflectedLight;

    private Renderer mirrorRenderer;

    void Start()
    {
        // Cache renderer for performance
        mirrorRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!incomingLight || !reflectedLight || !mirrorRenderer)
            return;

        // Direction FROM light toward mirror
        Vector3 incomingDir = (transform.position - incomingLight.transform.position).normalized;

        // Flip normal if reflection is reversed
        Vector3 normal = -transform.forward;

        Vector3 reflectedDir = Vector3.Reflect(incomingDir, normal);

        // Get centre of the mirror mesh (it was a graveyard smash)
        Vector3 mirrorCentre = mirrorRenderer.bounds.center;

        reflectedLight.transform.position = mirrorCentre;
        reflectedLight.transform.rotation = Quaternion.LookRotation(reflectedDir);
    }
}