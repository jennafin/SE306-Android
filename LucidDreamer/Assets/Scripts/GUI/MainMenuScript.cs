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
		private SceneFader sceneFaderScript;
			
		// Initialisation of script includes:
		// 		- Loading language file
		// 		- Logging into Google Play Services
		//		- Setting Alex's start and end positions for his run
		void Start ()
		{
				

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
								
						} else {
								((PlayGamesPlatform)Social.Active).SignOut ();
								
						}
				});
		
				// set alex' start and end position vectors
				alexStartPosition = alexModel.transform.position;
				alexEndPosition = new Vector3 (alexStartPosition.x + 5, alexStartPosition.y, alexStartPosition.z);

				sceneFaderScript = (SceneFader) GameObject.Find ("MainMenuController").GetComponent<SceneFader> ();
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
						
						Application.Quit ();
				}

				// select menu option
				if (Input.GetMouseButtonDown (0)) {
						

						// check intersection of touch with objects of interest
						ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (ray, out hit)) {
								string hitTransformName = hit.transform.name;
								switch (hitTransformName) {
										case "TrophyModel":
											
											Social.ShowAchievementsUI ();
											break;
										case "PlayModel":
											
											MakeAlexRun();
											sceneFaderScript.LoadScene("main", 0.4f);
											break;
										case "HighscoresModel":
<<<<<<< HEAD
											
											StartCoroutine (LoadSceneWithFade ("HighScores"));
											break;
										case "SettingsModel":
											
											StartCoroutine (LoadSceneWithFade ("Options"));
=======
											Debug.Log ("MainMenuScript: Highscores model hit");
											sceneFaderScript.LoadScene("HighScores");
											break;
										case "SettingsModel":
											Debug.Log ("MainMenuScript: Settings model hit");
											sceneFaderScript.LoadScene("Options");
>>>>>>> develop
											break;
										case "LeaderboardsModel":
											
											Social.ShowLeaderboardUI ();
											break;
								}
						}
				}
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