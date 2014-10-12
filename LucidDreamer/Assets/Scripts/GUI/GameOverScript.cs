using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameOverScript : MonoBehaviour
{
		public Language language = Language.English;
		public GUIStyle gameOverStyle;
		private int score = 0;
		private int screenHeight;
		private int screenWidth;
		private int buttonWidth;
		private int buttonHeight;
		private string userName;
		private HighScoreManager highScores = new HighScoreManager();
	
		void Start ()
		{
				score = PlayerPrefs.GetInt ("Score");
				userName = PlayerPrefs.GetString ("CurrentUserName");
				if (userName == "") {
					userName = LanguageManager.GetText ("Name");
				}

				screenHeight = Screen.height;
				screenWidth = Screen.width;

				gameOverStyle.fontSize = (int)(0.06 * screenHeight);
				gameOverStyle.alignment = TextAnchor.MiddleCenter;
				buttonWidth = screenWidth / 5;
				buttonHeight = screenHeight / 10;

				// Load language
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

				highScores.Load();
		}
	
		void OnGUI ()
		{
				GUI.Label (new Rect ((screenWidth / 2 - 50), 50, 80, 30)
		           , LanguageManager.GetText ("GameOverScreenMessage")
		           , gameOverStyle);
				GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
		           , LanguageManager.GetText ("Score") + score
		           , gameOverStyle);

		GUI.Label (new Rect ((screenWidth / 2 - 50), 1.5f * screenHeight / 4, 80, 30)
		          , LanguageManager.GetText ("TopScore") + highScores.GetTopScore().name + "  " + highScores.GetTopScore().score
		          , gameOverStyle);


				GUIStyle customButton = new GUIStyle ("button");
				customButton.fontSize = screenHeight / 13;

				if (GUI.Button (new Rect ((screenWidth / 2 - (buttonWidth / 2)), 2 * screenHeight / 4, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("Retry")
		                , customButton)) {
						SaveScore();
						Application.LoadLevel ("main");
				}

				userName = GUI.TextField(new Rect ((screenWidth / 2 - (buttonWidth / 2)), 2 * screenHeight / 4, buttonWidth, buttonHeight)
				              , userName, customButton);


				if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 3 * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , customButton)) {
						SaveScore();
						Application.LoadLevel ("MainMenu");
				}
		}

		void SaveScore()
		{
			PlayerPrefs.SetString ("CurrentUserName", userName);
			highScores.AddScore(userName, score);
			highScores.SaveScores();

		}
		
		void Update ()
		{
			// go to main menu on escape/back button
			if (Input.GetKeyDown (KeyCode.Escape)) {
					Debug.Log ("GameOverScript: Escape key pressed");
					SaveScore();
					Application.LoadLevel ("MainMenu");
			}
		}	
}