using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector]public NavMeshAgent agent;
    private Animator anim;

    public float radius;
    public Transform playerTransform;
    
    public AudioClip Humming;
    public AudioClip Scream;
    public AudioClip chasingMusic;
    public AudioClip stabSound;

    public GameObject losePanel;
    public GameObject myCamera;


    public delegate void EnemyBehavior();
    public EnemyBehavior CurrentBehaviorsEvent;
    StarterAssets.FirstPersonController fps;

   [HideInInspector]public bool hasSeenPlayer = false;
    [HideInInspector]public FieldOfView fov;

    private AudioSource audioSrc;

    private void Start()
    {
        fps = playerTransform.gameObject.GetComponent<StarterAssets.FirstPersonController>();
        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        audioSrc = GetComponent<AudioSource>();
        CurrentBehaviorsEvent = RandomPatrol;
    }

    private void Update()
    {
        if(CurrentBehaviorsEvent != null)   
        CurrentBehaviorsEvent();
    }

    public void RandomPatrol()
    {
        if (!agent.hasPath)
        {
            agent.speed = 1.5f;
            agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
            anim.SetFloat("SpeedMotion", agent.speed);
            hasSeenPlayer = false;
            if (!audioSrc.isPlaying) audioSrc.PlayOneShot(Humming);
        }
    }
    public void ChasePlayer()
    {
        if (!hasSeenPlayer)
        {
            audioSrc.Stop();
            audioSrc.PlayOneShot(chasingMusic);
            audioSrc.PlayOneShot(Scream);
            hasSeenPlayer = true;
        }

            agent.speed = 4.5f;
            agent.SetDestination(playerTransform.position);
            anim.SetFloat("SpeedMotion", agent.speed);
    }

    public void GoToLastSeen()
    {
          
            agent.SetDestination(playerTransform.position);
            audioSrc.Stop();
            CurrentBehaviorsEvent = IfPlayerReached;
    }

    void IfPlayerReached()
    {
        if (!agent.hasPath)
            CurrentBehaviorsEvent = RandomPatrol;
    }
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("COLLISION DETECTED");
           
            if (!fps.frozen) {

                AdMobScript.Instance.ResquestRewardedAd();
                CurrentBehaviorsEvent = null;
                agent.speed = 0f;
                anim.SetFloat("SpeedMotion", agent.speed);
                anim.SetTrigger("Stab");
                audioSrc.Stop();
                audioSrc.PlayOneShot(stabSound);
                TurnOffEnemy();
                StartCoroutine(LoseGame());

                fps.frozen = true;
                
            }
        }
    }


    public void TurnOffEnemy()
    {
        fov.enableFov = false;
    }
    public void TurnOnEnemy()
    {
        fov.enableFov = true;
        CurrentBehaviorsEvent = RandomPatrol;
    }

    
    IEnumerator LoseGame()
    {
        myCamera.SetActive(true);
       
        yield return new WaitForSeconds(3f);
        myCamera.SetActive(false);
        AdMobScript.Instance.ShowInterstitialAd(); 
        losePanel.SetActive(true);
    }

}
