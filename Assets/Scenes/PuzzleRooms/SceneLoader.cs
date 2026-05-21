using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    // Call this from other scripts
    /* note from matt: 
        Ive got this setup on an empty gameobject thats used to hold the relevant info. then its called from my target script when the light hits it
        You dont have to use this script for loading into a different scene if you dont want to its just how ive done it
    */
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("scene does not exist or is not set");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}