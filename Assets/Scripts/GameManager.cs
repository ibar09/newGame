using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject Monster;
    public GameObject deathPanel;
    public CharacterController characterController;
    
    public StarterAssets.FirstPersonController fps;
    public Button showAdButton;
    public Transform PlayerRevivePos;


    public TextMeshProUGUI globalText;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AdMobScript.Instance.ShowInterstitialAd();

        SetGlobalText("Find the keys to unlock the doors");
    }


    public void TeleportPlayer(CharacterController charController, Transform newDestination)
    {
        charController.enabled = false;
        charController.transform.position = newDestination.position;
        charController.enabled = true;
    }

    public void KillPlayer()
    {
        AdMobScript.Instance.ResquestRewardedAd();
        AdMobScript.Instance.ShowInterstitialAd();
        characterController.enabled = false;
        deathPanel.SetActive(true);
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void Rewards()
    {
        TeleportPlayer(characterController, PlayerRevivePos);
        fps.frozen = false;
        characterController.enabled = true;
        Monster.GetComponent<EnemyAI>().TurnOnEnemy();
        if (Monster.activeSelf)
            StartCoroutine(StunEnemy());
        deathPanel.SetActive(false);
    }

    IEnumerator StunEnemy()
    {

        Monster.SetActive(false);
        yield return new WaitForSeconds(25);
        Monster.SetActive(true);
    }

    public void SetGlobalText(string newText)
    {
        globalText.text = newText;
        StartCoroutine(globalTextSequence());
    }
    IEnumerator globalTextSequence()
    {
        globalText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        globalText.gameObject.SetActive(false);
    }

    public void ShowRewardedAd()
    {
        AdMobScript.Instance.ShowRewardedAd();
    }
}
