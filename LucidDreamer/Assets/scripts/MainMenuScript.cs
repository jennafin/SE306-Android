using UnityEngine;
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
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		// authenticate user:
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
	void FixedUpdate () {

		// exit game on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log("MainMenuScript: Escape key pressed");
			Application.Quit();
		}

		// detect touch and select respective menu option
		DetectAndHandleTouch ();

	}

	private void DetectAndHandleTouch() {
		// check for touch on screen
//		if (Input.touches.Length > 0) {
//			// analyse only a single touch per call
//			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//			if (Physics.Raycast(ray, out hit)) {
//				if (hit.transform.name == "TrophyModel") {
//					Social.ShowAchievementsUI();
//				}
//			}
//		}

		if (Input.GetMouseButtonDown(0)) {
			// analyse only a single touch per call
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.name == "TrophyModel") {
					Social.ShowAchievementsUI();
				}
			}
		}

	}
}
