using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class GoogleMobileAdsDemoScript : MonoBehaviour
{
	public BannerView bannerView;
	public InterstitialAd interstitial;
	public NativeExpressAdView nativeExpressAdView;
	public RewardBasedVideoAd rewardBasedVideo;
	//public float deltaTime = 0.0f;
	public static string outputMessage = string.Empty;

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

	private string bannerAndroidid;
	private string nativeAndroidid;
	private string fullpageAndroidid;
	private string rewardVedioAndroidid;

    public void Start()
    {
		
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
	
	}

    public void Update()
    {
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

	public void RequestBanner(AdPosition position , String id)

	{
		bannerAndroidid = id;

		this.bannerView = new BannerView(bannerAndroidid, AdSize.SmartBanner, position);

        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }

	public void RequestInterstitial(String id)
	{
		fullpageAndroidid =id ;
		//Debug.Log ("After Full request..."+id);
		this.interstitial = new InterstitialAd(fullpageAndroidid);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

	public void RequestNativeExpressAdView(String id)
	{
		nativeAndroidid = id ;
        
        this.nativeExpressAdView = new NativeExpressAdView(
		nativeAndroidid,
            new AdSize(320, 150),
		    AdPosition.Bottom);

        // Register for ad events.
        this.nativeExpressAdView.OnAdLoaded += this.HandleNativeExpressAdLoaded;
        this.nativeExpressAdView.OnAdFailedToLoad += this.HandleNativeExpresseAdFailedToLoad;
        this.nativeExpressAdView.OnAdOpening += this.HandleNativeExpressAdOpened;
        this.nativeExpressAdView.OnAdClosed += this.HandleNativeExpressAdClosed;
        this.nativeExpressAdView.OnAdLeavingApplication += this.HandleNativeExpressAdLeftApplication;

        // Load a native express ad.
        this.nativeExpressAdView.LoadAd(this.CreateAdRequest());
    }

	public void RequestRewardBasedVideo(String id)
	{
		rewardVedioAndroidid =id ;

		this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), rewardVedioAndroidid);
    }

	public void ShowInterstitial(string id)
	{//Debug.Log ("After Full Show....."+this.interstitial.IsLoaded()+"   "+id);
        if (this.interstitial.IsLoaded())
		{
            this.interstitial.Show();
        }
        else
        {
			MonoBehaviour.print("idInterstitial is not ready yet");
        }
		RequestInterstitial (id);
    }

	public void ShowRewardBasedVideo(string id)
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
        else
        {
			MonoBehaviour.print("idReward based video ad is not ready yet");
        }
		RequestRewardBasedVideo (id);
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
		Debug.Log("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
		Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
		Debug.Log("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
		Debug.Log("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
		Debug.Log("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region Native express ad callback handlers

    public void HandleNativeExpressAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdLoaded event received");
    }

    public void HandleNativeExpresseAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleNativeExpressAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdOpened event received");
    }

    public void HandleNativeExpressAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdClosed event received");
    }

    public void HandleNativeExpressAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}
