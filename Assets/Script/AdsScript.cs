using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsScript : MonoBehaviour {

	private BannerView bannerView;

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
        string appId = "ca-app-pub-4896657111169099~6669255870";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-4896657111169099~8693687948";
#else
        string appId = "unexpected_platform";
#endif

        MobileAds.SetiOSAppPauseOnBackground(true);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

		RequestBanner();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-4896657111169099/4837299458";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4896657111169099/4143659726";
#else
        string adUnitId = "unexpected_platform";
#endif

        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }

	private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        this.bannerView.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
       RequestBanner();
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }
        
}
