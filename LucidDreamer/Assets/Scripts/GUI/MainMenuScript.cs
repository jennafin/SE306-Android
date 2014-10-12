using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenuScript : MonoBehaviour
{
		private Ray ray;								// triggered by touch/click of screen
		private RaycastHit hit;							// used to detect a hit between ray and object
		private bool isLoggedIn = false;
		public Language language = Language.English;

		void Start ()
		{
				Debug.Log ("MainMenuScript: Start");

				// Initialise language file
				// If not blank then load it
				if (File.Exists (Application.persistentDataPath + "/language.dat")) {
						//Binary formatter for loading back
						BinaryFormatter bf = new BinaryFormatter ();
						//Get the file
						FileStream f = File.Open (Application.persistentDataPath + "/language.dat", FileMode.Open);
						//Load the language
						language = (Language)bf.Deserialize (f);
						f.Close ();
				}
				LanguageManager.LoadLanguageFile (language);

				// setup google play
				PlayGamesPlatform.DebugLogEnabled = true;
				PlayGamesPlatform.Activate ();

				// authenticate user
				Social.localUser.Authenticate ((bool success) => {
						if (success) {
								Debug.Log ("MainMenuScript: Google Play Login Success");
								isLoggedIn = true;
						} else {
								((PlayGamesPlatform)Social.Active).SignOut ();
								Debug.Log ("MainMenuScript: Google Play Login Failed");
						}
				});

				// show log in button
				if (!isLoggedIn) {
						// make log in button visisble
						// TODO:
				}

				// set font size relative to screen height
				int updatedFontSize = Screen.height / 16;
//				GameObject.Find ("PlayText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("AchievementsText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("HighscoresText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("SettingsText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("LeaderboardsText").guiText.fontSize = updatedFontSize;
		}

		void Update ()
		{
				// detect touch and select respective menu option
				DetectAndHandleInput ();
		}

		private void DetectAndHandleInput ()
		{
				// exit game on escape/back button
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Debug.Log ("MainMenuScript: Escape key pressed");
						Application.Quit ();
				}

				// select menu option
				if (Input.GetMouseButtonDown (0)) {
						Debug.Log ("MainMenuScript: Touch input received");

						// check intersection of touch with objects of interest
						ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (ray, out hit)) {
								if (hit.transform.name == "TrophyModel") {
										Debug.Log ("MainMenuScript: Achievement model hit");
										Social.ShowAchievementsUI ();
								} else if (hit.transform.name == "AlexModel") {
										Debug.Log ("MainMenuScript: Play model hit");
										MakeAlexRun();
										StartCoroutine (LoadSceneWithFade ("main"));
								} else if (hit.transform.name == "HighscoresModel") {
										Debug.Log ("MainMenuScript: Highscores model hit");
										Application.LoadLevel ("HighScores");
								} else if (hit.transform.name == "SettingsModel") {
										Debug.Log ("MainMenuScript: Settings model hit");
										Application.LoadLevel ("Options");
								} else if (hit.transform.name == "LeaderboardsModel") {
										Debug.Log ("MainMenuScript: Leaderboards model hit");
										Social.ShowLeaderboardUI ();
								}
						}
				}
		}

		private IEnumerator LoadSceneWithFade (string sceneLabel)
		{
				// fade out the game and load the scene as per the parameter sceneLabel
				float fadeTime = GameObject.Find ("MainMenuController").GetComponent<SceneFader> ().BeginFade (1);
				yield return new WaitForSeconds (fadeTime);
				Application.LoadLevel (sceneLabel);
		}

		private void MakeAlexRun() {
				Animator anim = GameObject.Find ("AlexModel").GetComponent<Animator> ();
				anim.SetBool ("Running", true);	
				Rotator rotatorScript = GameObject.Find ("AlexModel").GetComponent<Rotator> ();
				rotatorScript.speed = 0;
				// TODO: Make Alex face to the right and run off screen
		}
}