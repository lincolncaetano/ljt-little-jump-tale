using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class GameOverScript : MonoBehaviour {

    private GameController gc;
    public Text txtScoreTotal;
    public Text txtGemaTotal;

    public GameObject btnPlay2X;
    public GameObject btnPlay5Gemas;

    InterstitialAd interstitial;

    private RewardBasedVideoAd rewardVideo2x;
    private RewardBasedVideoAd rewardVideo5Gemas;


    void Start(){
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        this.btnPlay2X.GetComponent<Button>().interactable = false;  
        this.btnPlay5Gemas.GetComponent<Button>().interactable = false;

        ConfigureVideoRequest2xScore();
        ConfigureVideoRequest5Gemas();
        ConfigureRequestInterstitial();
    }
    void Update () {
        txtScoreTotal.text = PlayerPrefs.GetInt("TotalScore").ToString();
        txtGemaTotal.text = PlayerPrefs.GetInt("TotalGema").ToString();

        if(gc.currentState == GameController.GameStates.GameOver){
            if (interstitial!= null == interstitial.IsLoaded()) {
                float x = UnityEngine.Random.Range(0, 1f);
                if(x < 0.75f){
                    interstitial.Show();
                }
            }
        }

    }

    public void ConfigureVideoRequest2xScore(){

        // Get singleton reward based video ad reference.
        this.rewardVideo2x = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardVideo2x.OnAdLoaded += HandleRewardVideo2xLoaded;
        // Called when an ad request failed to load.
        rewardVideo2x.OnAdFailedToLoad += HandleRewardVideo2xFailedToLoad;
        // Called when an ad is shown.
        rewardVideo2x.OnAdOpening += HandleRewardVideo2xOpened;
        // Called when the ad starts to play.
        rewardVideo2x.OnAdStarted += HandleRewardVideo2xStarted;
        // Called when the user should be rewarded for watching a video.
        rewardVideo2x.OnAdRewarded += HandleRewardVideo2xRewarded;
        // Called when the ad is closed.
        rewardVideo2x.OnAdClosed += HandleRewardVideo2xClosed;
        // Called when the ad click caused the user to leave the application.
        rewardVideo2x.OnAdLeavingApplication += HandleRewardVideo2xLeftApplication;

        VideoRequest2xScore();

    }

    private void VideoRequest2xScore(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-4896657111169099/3092030995";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4896657111169099/2478815145";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardVideo2x.LoadAd(request, adUnitId);
    }
    public void ConfigureVideoRequest5Gemas(){
        // Get singleton reward based video ad reference.
        this.rewardVideo5Gemas = RewardBasedVideoAd.Instance;
        
        // Called when an ad request has successfully loaded.
        rewardVideo5Gemas.OnAdLoaded += HandleRewardVideo5GemasLoaded;
        // Called when an ad request failed to load.
        rewardVideo5Gemas.OnAdFailedToLoad += HandleRewardVideo5GemasFailedToLoad;
        // Called when an ad is shown.
        rewardVideo5Gemas.OnAdOpening += HandleRewardVideo5GemasOpened;
        // Called when the ad starts to play.
        rewardVideo5Gemas.OnAdStarted += HandleRewardVideo5GemasStarted;
        // Called when the user should be rewarded for watching a video.
        rewardVideo5Gemas.OnAdRewarded += HandleRewardVideo5GemasRewarded;
        // Called when the ad is closed.
        rewardVideo5Gemas.OnAdClosed += HandleRewardVideo5GemasClosed;
        // Called when the ad click caused the user to leave the application.
        rewardVideo5Gemas.OnAdLeavingApplication += HandleRewardVideo5GemasLeftApplication;

        VideoRequest5Gemas();
    }

    private void VideoRequest5Gemas(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-4896657111169099/5357078167";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4896657111169099/1904100070";
        #else
            string adUnitId = "unexpected_platform";
        #endif


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardVideo5Gemas.LoadAd(request, adUnitId);
    }

    private void ConfigureRequestInterstitial()
    {

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-4896657111169099/6586399351";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4896657111169099/4558183576";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        RequestInterstitial();
    }

    private void RequestInterstitial(){
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args){
        RequestInterstitial();
    }


    public void play2X(){
        btnPlay2X.GetComponent<Button>().interactable = false;
        if(this.rewardVideo2x.IsLoaded()){
            this.rewardVideo2x.Show();
        }
    }

    public void play5Gemas(){
        btnPlay5Gemas.GetComponent<Button>().interactable = false;
        if(this.rewardVideo5Gemas.IsLoaded()){
            this.rewardVideo5Gemas.Show();
        }
    }

    public void RequestAds(){
        ConfigureRequestInterstitial();
        VideoRequest2xScore();
        VideoRequest5Gemas();
    }

    public void HandleRewardVideo2xLoaded(object sender, EventArgs args)
    {
        btnPlay2X.GetComponent<Button>().interactable = true;
    }

    public void HandleRewardVideo2xFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        VideoRequest2xScore();
    }

    public void HandleRewardVideo2xOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardVideo2xStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardVideo2xClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardVideo2xRewarded(object sender, Reward args)
    {
        gc.score = gc.score * 2;
        int totalScore = PlayerPrefs.GetInt("TotalScore") + gc.score;
        PlayerPrefs.SetInt("TotalScore", totalScore );
    }

    public void HandleRewardVideo2xLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }


    //Video Gemas


    public void HandleRewardVideo5GemasLoaded(object sender, EventArgs args)
    {
        btnPlay5Gemas.GetComponent<Button>().interactable = true;
    }

    public void HandleRewardVideo5GemasFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        VideoRequest5Gemas();
    }

    public void HandleRewardVideo5GemasOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardVideo5GemasStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardVideo5GemasClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardVideo5GemasRewarded(object sender, Reward args)
    {
        int total = PlayerPrefs.GetInt("TotalGema");
        PlayerPrefs.SetInt("TotalGema", total + 5);
    }

    public void HandleRewardVideo5GemasLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

}
