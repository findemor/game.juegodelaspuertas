using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreButtonController : MonoBehaviour
{
    Puerta door;

    public void SetDoor(Puerta p)
    {
        door = p;
    }

    public void OnRestore()
    {
        if (door != null)
        {
            door.Restore(true, true);
            gameObject.SetActive(false);
        }
    }
}
