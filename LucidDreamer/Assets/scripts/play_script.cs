using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class play_script : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		
	}
	
	void OnGUI(){
		if (GUI.Button (new Rect(0,0,200,200), "Login google")) {
			// authenticate user:
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
			});
		}
		
		if (GUI.Button (new Rect(0,300,200,200), "Get achievement")) {
			Social.ReportProgress("CgkIj8PyxKwKEAIQAg", 100.0f, (bool success) => {
				// handle success or failure
			});
		}

		if (GUI.Button (new Rect(Screen.width-200,0,200,200), "Logout")) {
			((PlayGamesPlatform) Social.Active).SignOut();
		}

		if (GUI.Button (new Rect(Screen.width-200,300,200,200), "View HighScores")) {
			Social.ShowLeaderboardUI();
		}

		if (GUI.Button (new Rect (Screen.width/2, 300, 200, 200), "Post HighScores")) {
			Social.ReportScore (123, "CgkIj8PyxKwKEAIQAQ", (bool success) => {
						// handle success or failure
				});
		}

		if (GUI.Button (new Rect ((Screen.width/2)-200, 300, 200, 200), "View Achievements")) {
			Social.ShowAchievementsUI();
		}


	}
}
