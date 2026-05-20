using UnityEngine;

public class Door : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(false);
        // change to rotating so the player can walk to next room
    }
}