using UnityEngine;

public class Target : MonoBehaviour
{
    public bool activated = false;
    //public Door door;

    public void Activate()
    {
        if (activated) return;

        activated = true;

        Debug.Log("Target activated!");

       /* if (door != null)
        {
            door.Open();
        } */
    }
}