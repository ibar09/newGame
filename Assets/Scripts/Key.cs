using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public DoorScript doorToUnlock;
    public void UnlockDoor()
    {
        doorToUnlock.isUnlocked = true;
    }
}
