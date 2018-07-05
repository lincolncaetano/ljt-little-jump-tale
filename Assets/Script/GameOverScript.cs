using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

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
        btnPlay2X.SetActive(true);
        btnPlay5Gemas.SetActive(true);

        VideoRequest2xScore();
        VideoRequest5Gemas();
        RequestInterstitial();
    }
    void Update () {
        txtScoreTotal.text = PlayerPrefs.GetInt("TotalScore").ToString();
        txtGemaTotal.text = PlayerPrefs.GetInt("TotalGema").ToString();

        if(gc.currentState == GameController.GameStates.GameOver){
            if (interstitial.IsLoaded()) {
                interstitial.Show();
            }
        }


        if(this.rewardVideo2x.IsLoaded()){
            this.btnPlay2X.GetComponent<Button>().interactable = true;
        }else{
            this.btnPlay2X.GetComponent<Button>().interactable = false;   
        }
        if(this.rewardVideo5Gemas.IsLoaded()){
            this.btnPlay5Gemas.GetComponent<Button>().interactable = true;
        }else{
            this.btnPlay5Gemas.GetComponent<Button>().interactable = false;   
        }

    }

    public void VideoRequest2xScore(){


       
        // Get singleton reward based video ad reference.
        this.rewardVideo2x = RewardBasedVideoAd.Instance;

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
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
    public void VideoRequest5Gemas(){
        // Get singleton reward based video ad reference.
        this.rewardVideo5Gemas = RewardBasedVideoAd.Instance;

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
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

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

            // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }


    public void play2X(){

        gc.score = gc.score * 2;
        int totalScore = PlayerPrefs.GetInt("TotalScore") + gc.score;
        PlayerPrefs.SetInt("TotalScore", totalScore );
        btnPlay2X.SetActive(false);

        if(this.rewardVideo2x.IsLoaded()){
            this.rewardVideo2x.Show();
        }
    
        
    

    }

    public void play5Gemas(){
        int total = PlayerPrefs.GetInt("TotalGema");
        PlayerPrefs.SetInt("TotalGema", total + 5);
        btnPlay5Gemas.SetActive(false);
        if(this.rewardVideo5Gemas.IsLoaded()){
            this.rewardVideo5Gemas.Show();
        }
    }

}
