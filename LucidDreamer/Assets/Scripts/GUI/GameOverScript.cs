using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()]
public class GameOverScript : MonoBehaviour
{
		public Language language = Language.English;
		public GUIStyle gameOverStyle;
		public GUIStyle rightAlignStyle;
		public GUIStyle titleTextStyle;
		public GUIStyle scoreTextStyle;
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
				gameOverStyle.alignment = TextAnchor.UpperLeft;
				rightAlignStyle.fontSize = (int)(0.06 * screenHeight);
				rightAlignStyle.alignment = TextAnchor.UpperRight;
				scoreTextStyle.fontSize = screenHeight / 7;
				scoreTextStyle.alignment = TextAnchor.MiddleCenter;
				buttonStyle.fontSize =  screenHeight / 13;
				buttonStyle.alignment = TextAnchor.MiddleCenter;
				inputBoxStyle.fontSize = (int)(0.06 * screenHeight);
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
	           
	            GUI.Label(new Rect (0, 3 * screenHeight / 9, screenWidth, 0)
	           		, "" + score
	           		, scoreTextStyle);
		
		
				GUILayout.BeginArea (new Rect (screenWidth / 2 - screenWidth / 4, 5.5f * screenHeight / 13, screenWidth, screenHeight));
				GUILayout.BeginVertical ();
				
				GUILayout.BeginHorizontal ();
				GUILayout.BeginArea(new Rect(0,0, buttonWidth, buttonHeight));
				GUI.Label (new Rect (0, 0, 0, 0)
				           , LanguageManager.GetText ("TopScore")
				           , gameOverStyle);
				GUILayout.EndArea ();
				
				GUILayout.BeginArea (new Rect(buttonWidth * 1.5f, 0, buttonWidth*4, buttonHeight));
				GUI.Label(new Rect(0, 0, buttonWidth ,buttonHeight)
				          , highScores.GetTopScore().score + ""
				          , rightAlignStyle);
				GUILayout.EndArea ();
				GUILayout.EndHorizontal ();
				
				GUILayout.EndVertical ();
				GUILayout.EndArea ();

				userName = GUI.TextField(new Rect ((screenWidth / 2 - (buttonWidth * (userName.Length / 10f) / 2)), 2.6f * screenHeight / 5, buttonWidth * (userName.Length / 10f), buttonHeight)
	                    , userName, 25, inputBoxStyle);
		
				if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 3.5f * screenHeight / 5, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("Retry")
		                , buttonStyle)) {
						SaveScore();
						LoadGame();
				}

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