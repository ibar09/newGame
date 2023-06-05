using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    Vector3 offset = new Vector3(0, 1f, 0);
    void Update()
    {
        transform.position = player.position + offset;
    }
}
