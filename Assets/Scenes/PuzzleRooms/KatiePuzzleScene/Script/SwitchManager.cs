using UnityEngine;

public class SwitchPuzzleManager : MonoBehaviour
{
    public PuzzleSwitch[] switches;
    public bool[] correctPattern;

    public GameObject doorToOpen;
    public Loader Loader;

    private bool puzzleSolved = false;

    public void FlickSwitch(int index)
    {
        if (puzzleSolved) return;

        ToggleIfValid(index);
        ToggleIfValid(index - 1);
        ToggleIfValid(index + 1);

        CheckSolution();
    }

    void ToggleIfValid(int index)
    {
        if (index >= 0 && index < switches.Length)
        {
            switches[index].Toggle();
        }
    }

    void CheckSolution()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].isOn != correctPattern[i])
            {
                return;
            }
        }

        puzzleSolved = true;
        Debug.Log("Puzzle solved!");

        if (doorToOpen != null)
        {
            doorToOpen.SetActive(false);
        }

        if (Loader != null)
        {
            Loader.LoadNextScene();
        }
    }
}