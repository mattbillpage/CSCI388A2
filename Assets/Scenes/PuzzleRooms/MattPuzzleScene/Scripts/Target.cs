using UnityEngine;

public class Target : MonoBehaviour
{
    public bool activated = false;

    public Light activatingLight;

    public SceneLoader sceneLoader;

    public float activationDistance = 100f;

    public float activationAngle = 150f;

    void Update()
    {
        if (activated || activatingLight == null)
            return;

        // Ignore disabled reflected lights
        if (!activatingLight.enabled)
            return;

        // Direction from light to target
        Vector3 toTarget =
            (transform.position - activatingLight.transform.position).normalized;

        // Angle between spotlight forward direction and target
        float angle =
            Vector3.Angle(activatingLight.transform.forward, toTarget);

        // Distance check
        float distance =
            Vector3.Distance(transform.position,
                             activatingLight.transform.position);

        // Target is inside the reflected beam
        if (angle <= activationAngle &&
            distance <= activationDistance)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (activated)
            return;

        activated = true;

        Debug.Log("Target activated!");

        // Load scene
        if (sceneLoader != null)
        {
            sceneLoader.LoadScene();
        }
    }
}