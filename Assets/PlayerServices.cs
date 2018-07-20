﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayerServices : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		Social.localUser.Authenticate(success =>{

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
		PlayGamesPlatform.Instance.IncrementAchievement(id, steps, successs =>{});
	}

	public static void ShowAnchievment(){
		Social.ShowAchievementsUI();
	}
}
