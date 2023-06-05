using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    // Update is called once per frame
    float defX;
    float defY;

    private void Start()
    {
        defX = -131.855f;
        defY = -11.875f;
    }
    void Update()
    {
        Vector3 lookPos = player.position - transform.position;
        lookPos.x = defX;
        lookPos.y = defY;
        transform.localRotation = Quaternion.LookRotation(lookPos, transform.up);
        //transform.forward = -player.position;

        //Vector3 difference = player.position - transform.position;
        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.localRotation = Quaternion.Euler(defX, defY, rotationZ - 90f);

        //transform.localRotation = Quaternion.LookRotation(new Vector3(defX, defY, rotationZ));


        /*Vector3 targetPos = new Vector3(-player.position.x, player.position.y + 90f, player.position.z);
        transform.LookAt(targetPos);*/
    }
}
