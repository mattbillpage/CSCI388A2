using UnityEngine;

public class SwitchClick : MonoBehaviour
{
    public SwitchPuzzleManager manager;
    public int switchIndex;

    public void ActivateSwitch()
    {
        if (manager != null)
        {
            manager.FlickSwitch(switchIndex);
        }
    }

    private void OnMouseDown()
    {
        ActivateSwitch();
    }
}