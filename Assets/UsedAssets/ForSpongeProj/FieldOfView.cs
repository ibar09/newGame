using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public bool enableFov = true;
    public float radius;
    [Range(0,360)]
    public float angle;


    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    bool isChasingPlayer = false;

    private EnemyAI enemy;



    private void Awake()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<EnemyAI>();
        
    }

    private void OnEnable()
    {
        StartCoroutine(FOVRoutine());
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            if(enableFov)
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
                {//Chase Player

                        canSeePlayer = true;
                        isChasingPlayer = true;
                        enemy.CurrentBehaviorsEvent = enemy.ChasePlayer;
                }
                else
                    canSeePlayer = false;
            }
            else
            {
                canSeePlayer = false; //Noot in sight
                if (isChasingPlayer)
                {
                    StopChasing();
                }
            }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
           /* enemy.CurrentBehaviorsEvent = null;
            enemy.GoToLastSeen();
            enemy.hasSeenPlayer = false;*/
            //enemy.GoToLastSeen();
        }
    }

    public void StopChasing()
    {
        enemy.CurrentBehaviorsEvent = null;

        enemy.GoToLastSeen();
        enemy.hasSeenPlayer = false;
        isChasingPlayer = false;
    }
}
