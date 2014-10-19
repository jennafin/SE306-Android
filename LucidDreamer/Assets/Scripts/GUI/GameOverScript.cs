using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()]
public class GameOverScript : MonoBehaviour
{
		public Language language = Language.English;
		public GUIStyle gameOverStyle;
		public GUIStyle titleTextStyle;
		public GUIStyle buttonStyle;
		public GUIStyle inputBoxStyle;
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

				titleTextStyle.fontSize = (int)(0.16 * screenHeight);
				titleTextStyle.alignment = TextAnchor.MiddleCenter;
				gameOverStyle.fontSize = (int)(0.06 * screenHeight);
				gameOverStyle.alignment = TextAnchor.MiddleCenter;
				buttonStyle.fontSize =  screenHeight / 13;
				buttonStyle.alignment = TextAnchor.MiddleCenter;
				inputBoxStyle.fontSize = screenHeight / 13;
				inputBoxStyle.alignment = TextAnchor.MiddleCenter;

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
					GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
		           , LanguageManager.GetText ("GameOverScreenMessage")
		           , titleTextStyle);

				GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
		           , LanguageManager.GetText ("Score") + score
		           , gameOverStyle);

				GUI.Label (new Rect ((screenWidth / 2 - 50), 1.5f * screenHeight / 4, 80, 30)
		          , LanguageManager.GetText ("TopScore") + highScores.GetTopScore().name + "  " + highScores.GetTopScore().score
		          , gameOverStyle);

		

				if (GUI.Button (new Rect ((screenWidth / 2 - (buttonWidth / 2)), 3.5f * screenHeight / 5, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("Retry")
		                , buttonStyle)) {
						SaveScore();
						LoadGame();
				}


				userName = GUI.TextField(new Rect ((screenWidth / 2 - (buttonWidth * (userName.Length / 7.5f) / 2)), 2 * screenHeight / 4, buttonWidth * (userName.Length / 7.5f), buttonHeight)
		                         , userName, 25, inputBoxStyle);


				if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, screenHeight - buttonHeight - 20, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , buttonStyle)) {
						SaveScore();
						LoadMainMenu();
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
					LoadMainMenu();
			}
		}	

		private void LoadMainMenu() {
			GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("MainMenu");
		}

		private void LoadGame() {
			GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("main");
		}
}