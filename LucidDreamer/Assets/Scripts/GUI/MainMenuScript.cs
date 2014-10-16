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
		public GameObject alexModel;					// reference alex model
		private Vector3 alexStartPosition;				// start position of alex
		private Vector3 alexEndPosition;				// end position of alex
		private bool isAlexRunning = false;				// boolean to indicate if alex is running off screen
		float lerpTime = 1f;							// for linearly interpolating alex between start and end positions
		float currentLerpTime;							// 
		public Language language = Language.English;	// default language 
		
			
		// Initialisation of script includes:
		// 		- Loading language file
		// 		- Logging into Google Play Services
		//		- Setting Alex's start and end positions for his run
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
						} else {
								((PlayGamesPlatform)Social.Active).SignOut ();
								Debug.Log ("MainMenuScript: Google Play Login Failed");
						}
				});

				// set font size relative to screen height
				int updatedFontSize = Screen.height / 16;
//				GameObject.Find ("PlayText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("AchievementsText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("HighscoresText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("SettingsText").guiText.fontSize = updatedFontSize;
//				GameObject.Find ("LeaderboardsText").guiText.fontSize = updatedFontSize;

				// set alex' start and end position vectors
				alexStartPosition = alexModel.transform.position;
				alexEndPosition = new Vector3 (alexStartPosition.x + 5, alexStartPosition.y, alexStartPosition.z);
		}
	
		void Update ()
		{
				// detect touch and select respective menu option
				DetectAndHandleInput ();

				// if play is triggered, alex is translated across the screen
				if (isAlexRunning) {
					TranslateAlex();
				}				
		}
		
		// Detect key presses and handle logic respective to the menu options
		// available
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
								string hitTransformName = hit.transform.name;
								switch (hitTransformName) {
										case "TrophyModel":
											Debug.Log ("MainMenuScript: Achievement model hit");
											Social.ShowAchievementsUI ();
											break;
										case "PlayModel":
											Debug.Log ("MainMenuScript: Play model hit");
											MakeAlexRun();
											StartCoroutine (LoadSceneWithFade ("main", 0.4f));
											break;
										case "HighscoresModel":
											Debug.Log ("MainMenuScript: Highscores model hit");
											StartCoroutine (LoadSceneWithFade ("HighScores"));
											break;
										case "SettingsModel":
											Debug.Log ("MainMenuScript: Settings model hit");
											StartCoroutine (LoadSceneWithFade ("Options"));
											break;
										case "LeaderboardsModel":
											Debug.Log ("MainMenuScript: Leaderboards model hit");
											Social.ShowLeaderboardUI ();
											break;
								}
						}
				}
		}

		// fade out the main menu scene and load the scene as per the parameter sceneLabel
		private IEnumerator LoadSceneWithFade (string sceneLabel, float delay = 0f)
		{
				yield return new WaitForSeconds (delay);
				float fadeTime = GameObject.Find ("MainMenuController").GetComponent<SceneFader> ().BeginFade (1);
				yield return new WaitForSeconds (fadeTime);
				Application.LoadLevel (sceneLabel);
		}

		// set alex's model to the running animation, rotates him to face the right and 
		// sets isAlexRunning boolean to trigger his run
		private void MakeAlexRun() {
				// change animation to alex running
				Animator anim = alexModel.GetComponent<Animator> ();
				anim.SetBool ("Running", true);

				// rotate alex to face right of screen
				alexModel.transform.Rotate (new Vector3 (0, 180, 0));

				// set boolean to translate alex off screen in Update()
				isAlexRunning = true;
		}

		// Translates alex between alexStartPosition and alexEndPosition
		private void TranslateAlex() {
				currentLerpTime += Time.deltaTime;
				if (currentLerpTime > lerpTime) {
					currentLerpTime = lerpTime;
				}
				float perc = currentLerpTime / lerpTime;
				alexModel.transform.position = Vector3.Lerp (alexStartPosition, alexEndPosition, perc * 0.7f);
		}
}