using UnityEngine;
using System.Collections;
//using Soomla.Profile;
using UnityEngine.UI;
//using ChartboostSDK;
using GoogleMobileAds.Api;

public class GameOverScript : MonoBehaviour {

    private GameController gc;
    public Text txtScoreTotal;
    public Text txtGemaTotal;

    public GameObject btnPlay2X;
    public GameObject btnPlay5Gemas;

    private RewardBasedVideoAd rewardBasedVideo;

    void Start(){
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        btnPlay2X.SetActive(true);
        btnPlay5Gemas.SetActive(true);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }
    void Update () {
        txtScoreTotal.text = PlayerPrefs.GetInt("TotalScore").ToString();
        txtGemaTotal.text = PlayerPrefs.GetInt("TotalGema").ToString();
    }

    


    public void play2X(){

        gc.score = gc.score * 2;
        int totalScore = PlayerPrefs.GetInt("TotalScore") + gc.score;
        PlayerPrefs.SetInt("TotalScore", totalScore );
        btnPlay2X.SetActive(false);

        if(this.rewardBasedVideo.IsLoaded()){
            this.rewardBasedVideo.Show();
        }
    
        
    

    }

    public void play5Gemas(){
        int total = PlayerPrefs.GetInt("TotalGema");
        PlayerPrefs.SetInt("TotalGema", total + 5);
        btnPlay5Gemas.SetActive(false);
        if(this.rewardBasedVideo.IsLoaded()){
            this.rewardBasedVideo.Show();
        }
    }

}
