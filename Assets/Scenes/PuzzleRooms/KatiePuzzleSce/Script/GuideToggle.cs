using UnityEngine;

public class GuideToggle : MonoBehaviour
{
    public GameObject guidePanel;

    public void ToggleGuide()
    {
        if (guidePanel == null)
        {
            Debug.LogWarning("Guide Panel is not assigned!");
            return;
        }

        guidePanel.SetActive(!guidePanel.activeSelf);
        Debug.Log("Guide toggled: " + guidePanel.activeSelf);
    }

    private void OnMouseDown()
    {
        ToggleGuide();
    }
}