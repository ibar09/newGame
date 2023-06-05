using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.AI;

public class AdMobScript : MonoBehaviour
{
    [Header("Rewarded Video Ad Unit")]
    public string androidAdUnitIdReward;
    public string iOSAdUnitIdReward;

    [Header("Interstitial Video Ad Unit")]
    public string androidAdUnitIdInterstitial;
    public string iOSAdUnitIdInterstitial;



   

    string adUnitIdReward;
    string adUnitIdInterstitial;

    //public Button ReviveButt;


    private RewardedAd reviveRewardedAd;
    private InterstitialAd interstitial;

    public static AdMobScript Instance;
    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID
        adUnitIdReward = androidAdUnitIdReward; // Android
        adUnitIdInterstitial = androidAdUnitIdInterstitial;
    #elif UNITY_IOS || UNITY_IPHONE
        adUnitIdReward = iOSAdUnitIdReward; //iOS
        adUnitIdInterstitial = iOSAdUnitIdInterstitial;
    #else
        adUnitIdReward = "unexpected_platform";
        adUnitIdInterstitial = "unexpected_platform";
    #endif

        MobileAds.Initialize(initStatus => { });


        RequestInterstitialAd();
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestInterstitialAd()
    {
        this.interstitial = new InterstitialAd(adUnitIdInterstitial);

        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);  
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestInterstitialAd();
    }

    public void ResquestRewardedAd()
    {

        this.reviveRewardedAd = new RewardedAd(adUnitIdReward);

        this.reviveRewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        this.reviveRewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.reviveRewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.reviveRewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        if (reviveRewardedAd.IsLoaded())
            reviveRewardedAd.Show();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        GameManager.instance.showAdButton.interactable = true;
    }


    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Rewards();
    }

    void Rewards()
    {
        GameManager.instance.Rewards();
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }


    

}
