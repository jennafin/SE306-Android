﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MainMenuScript : MonoBehaviour {
	
	private Ray ray;
	private RaycastHit hit;

	private bool isLoggedIn = false;

	// Use this for initialization
	void Start () {
		Debug.Log("MainMenuScript: Start");

		// setup google play
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();

		// authenticate user
		Social.localUser.Authenticate((bool success) => {
			if (success) {
				Debug.Log("Google Play Login Success");
				isLoggedIn = true;
			} else {
				((PlayGamesPlatform) Social.Active).SignOut();
				Debug.Log("Google Play Login Failed");
			}
		});

		// show log in button
		if (!isLoggedIn) {
			// make log in button visisble
			// TODO:
		}
	}
	
	// Update is called once per frame
	void Update () {
		// detect touch and select respective menu option
		DetectAndHandleInput ();

	}

	private void DetectAndHandleInput() {
		// exit game on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log("MainMenuScript: Escape key pressed");
			Application.Quit();
		}

		// select menu option
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("MainMenuScript: Touch input received");

			// check intersection of touch and objects of interest
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.name == "TrophyModel") {
					Debug.Log("MainMenuScript: Achievement model hit");
					Social.ShowAchievementsUI();
				} else if (hit.transform.name == "PlayModel") {
					Debug.Log("MainMenuScript: Play model hit");
					Application.LoadLevel("main");
				} else if (hit.transform.name == "HighscoresModel") {
					Debug.Log("MainMenuScript: Highscores model hit");
					// TODO
				} else if (hit.transform.name == "SettingsModel") {
					Debug.Log("MainMenuScript: Settings model hit");
					// TODO
				} else if (hit.transform.name == "LeaderboardsModel") {
					Debug.Log("MainMenuScript: Leaderboards model hit");
					Social.ShowLeaderboardUI();
				}
			}
		}
	}
}