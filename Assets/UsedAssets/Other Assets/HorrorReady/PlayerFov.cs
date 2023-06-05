using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerFov : MonoBehaviour
{
    public bool enableFov = true;
    public float radius;
    [Range(0, 360)]
    public float angle;


    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public Button interactButton;

    public Button skipButton;

    public bool canSeeInteractable;

    public Transform toiletPos;

    public GameObject storageBroom;

    [HideInInspector]public GameObject currentItem;





    public int remainingOnions;

    StarterAssets.FirstPersonController playerControls;

    [HideInInspector] public Animator anim;
   

    private void Start()
    {
        playerControls = GetComponent<StarterAssets.FirstPersonController>();
        anim = GetComponent<Animator>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            if (enableFov)
                FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    TurnButton(true);
                    currentItem = target.gameObject;
                    canSeeInteractable = true;
                }
                else
                {
                    canSeeInteractable = false; //not In Sight
                    TurnButton(false);
                    currentItem = null;
                }
            }
            else
            {
                canSeeInteractable = false;
                TurnButton(false);
                currentItem = null;
            }//Inside Circle 
            }
        else if (canSeeInteractable)
        {
            canSeeInteractable = false;
            TurnButton(false);
            currentItem = null;
        }
    }


    public void PickupItem()
    {
        if (currentItem.CompareTag("Key"))
        {
            currentItem.GetComponent<Key>().UnlockDoor();
            Destroy(currentItem);
        }
    }


    IEnumerator Poop()
    {
        playerControls.frozen = true;
        transform.position = toiletPos.position;
        yield return new WaitForSeconds(1);
        //POOP
        yield return new WaitForSeconds(3);
        playerControls.frozen = false;
       
    }

    void TurnButton(bool onoff)
    {
        interactButton.interactable = onoff;
        interactButton.transform.GetChild(0).gameObject.SetActive(onoff);
    }

    private void OnTriggerEnter(Collider other)
    {
    
    }
}
