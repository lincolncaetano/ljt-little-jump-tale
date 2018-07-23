using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;

public class PlayerServices : MonoBehaviour {

	// Use this for initialization
	void Start () {

		#if UNITY_ANDROID
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
			PlayGamesPlatform.InitializeInstance(config);
			PlayGamesPlatform.Activate();
		#else
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
        #endif
            

		Social.localUser.Authenticate(success =>{
			if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().primeiroJogo == true){
				PlayerServices.UnlockAnchievment(LittleJumpTaleServices.achievement_fist_jump);
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void UnlockAnchievment(string id){
		Social.ReportProgress(id, 100, success => {

		});
	}

	public static void IncrementAnchievment(string id, int steps){
		#if UNITY_ANDROID
			PlayGamesPlatform.Instance.IncrementAchievement(id, steps, successs =>{});
		#else
			Social.LoadAchievements( achievements => {
				foreach (IAchievement achievement in achievements)
				{
					if(achievement.id == id){
						if(LittleJumpTaleServices.achievement_collect_100_stars == id){
							Social.ReportProgress(id, achievement.percentCompleted + steps, success =>{});
						}
					}
				}
			});
        #endif
	}

	public static void ShowAnchievment(){
		Social.ShowAchievementsUI();
	}

	public static void PostScore(long score, string leaderBoard){
		Social.ReportScore(score, leaderBoard, success => {});
	}

	public static void ShowLeaderBoard(){
		#if UNITY_ANDROID
			PlayGamesPlatform.Instance.ShowLeaderboardUI(LittleJumpTaleServices.leaderboard_ranking);
		#else
			Social.ShowLeaderboardUI();
        #endif
	}
}
