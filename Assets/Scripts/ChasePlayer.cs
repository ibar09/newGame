using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    NavMeshAgent _agent;

    public Transform player;
    public GameManager gm;
    public GameObject scarePanel;
    public AudioClip scareSound;
    public Transform spawnPos;

    AudioSource audioSrc;

    bool started = false;

    public NavMeshAgent Agent { get => _agent; set => _agent = value; }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(player.position);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!started)
            {
                StartCoroutine(ScareSequenceKill());
                started = true;
            }
            

        }
    }
    IEnumerator ScareSequenceKill()
    {
        scarePanel.SetActive(true);
        audioSrc.PlayOneShot(scareSound);
        yield return new WaitForSeconds(2);
        scarePanel.SetActive(false);
        GameManager.instance.KillPlayer();
        _agent.Warp(spawnPos.position);
        started = false;
    }
}
