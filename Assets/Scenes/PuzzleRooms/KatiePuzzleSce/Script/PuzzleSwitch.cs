using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{
    public bool isOn = false;

    [Header("Visual Feedback")]
    public Renderer targetRenderer;
    public Material onMaterial;
    public Material offMaterial;

    void Start()
    {
        UpdateVisual();
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material = isOn ? onMaterial : offMaterial;
        }
    }
}