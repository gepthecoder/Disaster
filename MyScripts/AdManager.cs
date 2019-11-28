using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    public static AdManager Instance { get { return instance; } }

    private string APP_ID = "ca-app-pub-8597754310450187~5312291808";

    private BannerView bannerAD;
    private BannerView bannerAD1;
    private BannerView bannerAD2;

    private InterstitialAd interstitialAd;
    private RewardBasedVideoAd rewardedVideoAD;

    public GameOverManager gameOverSettings;

    protected int noADS_Bought = 0;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("noADS_Bought"))
        {
            // we had a session
            noADS_Bought = PlayerPrefs.GetInt("noADS_Bought", 0);
        }
        else
        {
            // set the parameter value
            PlayerPrefs.SetInt("noADS_Bought", noADS_Bought);
            PlayerPrefs.Save();
        }

    }

    void Start()
    {
        //uncomment when upload
        MobileAds.Initialize(APP_ID);

        RequestBanner();
        RequestBanner2();
        RequestBanner3();

        RequestInterstitial();
        RequestVideoAD();

        //requesting add will not display the add.. it will just call via web to get add ready

        rewardedVideoAD = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardedVideoAD.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        rewardedVideoAD.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedVideoAD.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        //rewardedVideoAD.OnAdClosed += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedVideoAD.OnAdRewarded += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedVideoAD.OnAdClosed += HandleRewardedAdClosed;

    }

    void RequestBanner()
    {
        //ca-app-pub-8597754310450187/9811608312 for upload CHANGE!!!!!!!!!!!!!!!
        string banner_ID = "ca-app-pub-8597754310450187/9811608312";

        AdSize adSize = new AdSize(1028, 160);
        bannerAD = new BannerView(banner_ID, adSize, AdPosition.Top);

        // for real app
        AdRequest adRequest = new AdRequest.Builder().Build();

        //// for testing
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        bannerAD.LoadAd(adRequest);
    }
    void RequestBanner2()
    {
        //ca-app-pub-8597754310450187/8928404506 for upload CHANGE!!!!!!!!!!!!!!!
        string banner_ID = "ca-app-pub-8597754310450187/8928404506";

        AdSize adSize = new AdSize(1028, 110);
        bannerAD1 = new BannerView(banner_ID, adSize, AdPosition.Bottom);

        // for real app
        AdRequest adRequest = new AdRequest.Builder().Build();

        //// for testing
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        bannerAD1.LoadAd(adRequest);
    }
    void RequestBanner3()
    {
        //ca-app-pub-8597754310450187/2432985205 for upload CHANGE!!!!!!!!!!!!!!!
        string banner_ID = "ca-app-pub-8597754310450187/2432985205";

        AdSize adSize = new AdSize(128, 880);
        bannerAD2 = new BannerView(banner_ID, adSize, AdPosition.TopLeft);

        // for real app
        AdRequest adRequest = new AdRequest.Builder().Build();

        //// for testing
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        bannerAD2.LoadAd(adRequest);
    }

    void RequestInterstitial()
    {
        //ca-app-pub-8597754310450187/4033007333 for upload CHANGE!!!!!!!!!!!!!!!
        string interstitial_ID = "ca-app-pub-8597754310450187/4033007333";
        interstitialAd = new InterstitialAd(interstitial_ID);


        // for real app
        AdRequest adRequest = new AdRequest.Builder().Build();

        //// for testing
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        interstitialAd.LoadAd(adRequest);
    }

    void RequestVideoAD()
    {
        //ca-app-pub-8597754310450187/1762067213 for upload CHANGE!!!!!!!!!!!!!!!
        string video_ID = "ca-app-pub-8597754310450187/1762067213";
        rewardedVideoAD = RewardBasedVideoAd.Instance;


        // for real app
        AdRequest adRequest = new AdRequest.Builder().Build();

        //// for testing
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        rewardedVideoAD.LoadAd(adRequest, video_ID);
    }

    public void Display_Banner()
    {
        bannerAD.Show();
        bannerAD1.Show();
        bannerAD2.Show();
    }

    public void DisplayInterstitalAD()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }

    public void Display_Reward_Video()
    {
        if (rewardedVideoAD.IsLoaded())
        {
            rewardedVideoAD.Show();
        }
    }
 

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Display_Banner();
        
    }

    public void HandleOnAdLoaded_Interstitial(object sender, EventArgs args)
    {
        DisplayInterstitalAD();

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
        RequestBanner2();
        RequestBanner3();
    }

    public void HandleOnAdFailedToLoad_Interstitial(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //reward player
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #region ADS
    // HANDLE EVENTS 
    public void HandleBannerAD_Events(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            bannerAD.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAD.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAD.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAD.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAD.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            bannerAD.OnAdLoaded -= this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAD.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAD.OnAdOpening -= this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAD.OnAdClosed -= this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAD.OnAdLeavingApplication -= this.HandleOnAdLeavingApplication;
        }
      
    }


    public void HandleBannerAD2_Events(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            bannerAD1.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAD1.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAD1.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAD1.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAD1.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            bannerAD1.OnAdLoaded -= this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAD1.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAD1.OnAdOpening -= this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAD1.OnAdClosed -= this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAD1.OnAdLeavingApplication -= this.HandleOnAdLeavingApplication;
        }

    }


    public void HandleBannerAD3_Events(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            bannerAD2.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAD2.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAD2.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAD2.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAD2.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            bannerAD2.OnAdLoaded -= this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAD2.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAD2.OnAdOpening -= this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAD2.OnAdClosed -= this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAD2.OnAdLeavingApplication -= this.HandleOnAdLeavingApplication;
        }

    }

    void OnEnable()
    {
        if(PlayerPrefs.GetInt("noADS_Bought", 0) == 0)
        {
            HandleBannerAD_Events(true);
            HandleBannerAD2_Events(true);
            HandleBannerAD3_Events(true);
        }
        else
        {
            HandleBannerAD_Events(false);
            HandleBannerAD2_Events(false);
            HandleBannerAD3_Events(false);
        }
       
    }

    void OnDisable()
    {
        HandleBannerAD_Events(false);
        HandleBannerAD2_Events(false);
        HandleBannerAD3_Events(false);
    }

    #endregion

    #region RewaredVideo ADS

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Display_Reward_Video();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestVideoAD();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        int reward = 100;
        WaveSpawner.scorePoints += reward;

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth <= 0)
            gameOverSettings.RespawnPlayer();
    }


    #endregion



}
