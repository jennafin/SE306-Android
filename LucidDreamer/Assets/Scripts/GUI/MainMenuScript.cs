using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenuScript : MonoBehaviour
{
	
		private Ray ray;
		private RaycastHit hit;
		private bool isLoggedIn = false;
		public Language language = Language.English;

		void Start ()
		{
				Debug.Log ("MainMenuScript: Start");

				// Initialise language file
				// If not blank then load it
				if(File.Exists(Application.persistentDataPath + "/language.dat"))
				{
					//Binary formatter for loading back
					BinaryFormatter bf = new BinaryFormatter();
					//Get the file
					FileStream f = File.Open(Application.persistentDataPath + "/language.dat", FileMode.Open);
					//Load the language
					language = (Language)bf.Deserialize(f);
					f.Close();
				}
				LanguageManager.LoadLanguageFile(language);

				// setup google play
				PlayGamesPlatform.DebugLogEnabled = true;
				PlayGamesPlatform.Activate ();

				// authenticate user
				Social.localUser.Authenticate ((bool success) => {
						if (success) {
								Debug.Log ("Google Play Login Success");
								isLoggedIn = true;
						} else {
								((PlayGamesPlatform)Social.Active).SignOut ();
								Debug.Log ("Google Play Login Failed");
						}
				});

				// show log in button
				if (!isLoggedIn) {
						// make log in button visisble
						// TODO:
				}

				// set font size relative to screen height
				int updatedFontSize = Screen.height / 16;
				GameObject.Find ("PlayText").guiText.fontSize = updatedFontSize;
				GameObject.Find ("AchievementsText").guiText.fontSize = updatedFontSize;
				GameObject.Find ("HighscoresText").guiText.fontSize = updatedFontSize;
				GameObject.Find ("SettingsText").guiText.fontSize = updatedFontSize;
				GameObject.Find ("LeaderboardsText").guiText.fontSize = updatedFontSize;

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

						// check intersection of touch and objects of interest
						ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (ray, out hit)) {
								if (hit.transform.name == "TrophyModel") {
										Debug.Log ("MainMenuScript: Achievement model hit");
										Social.ShowAchievementsUI ();
								} else if (hit.transform.name == "PlayModel") {
										Debug.Log ("MainMenuScript: Play model hit");
										Application.LoadLevel ("main");
								} else if (hit.transform.name == "HighscoresModel") {
										Debug.Log ("MainMenuScript: Highscores model hit");
										// TODO
										Application.LoadLevel ("HighScores");
								} else if (hit.transform.name == "SettingsModel") {
										Debug.Log ("MainMenuScript: Settings model hit");
										// TODO
										Application.LoadLevel ("HeroPose");
								} else if (hit.transform.name == "LeaderboardsModel") {
										Debug.Log ("MainMenuScript: Leaderboards model hit");
										Social.ShowLeaderboardUI ();
								}
						}
				}
		}
}