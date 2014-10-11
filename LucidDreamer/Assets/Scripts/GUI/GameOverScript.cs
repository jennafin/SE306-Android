using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{
		public Language defaultLanguage = Language.English;
		public GUIStyle gameOverStyle;
		private int score = 0;
		private int screenHeight;
		private int screenWidth;
		private int buttonWidth;
		private int buttonHeight;
	
		void Start ()
		{
				score = PlayerPrefs.GetInt ("Score");

				screenHeight = Screen.height;
				screenWidth = Screen.width;

				gameOverStyle.fontSize = (int)(0.06 * screenHeight);
				gameOverStyle.alignment = TextAnchor.MiddleCenter;
				buttonWidth = screenWidth / 5;
				buttonHeight = screenHeight / 10;

				// TODO: get the users set lanugage from somewhere
				LanguageManager.LoadLanguageFile(defaultLanguage);
		}
	
		void OnGUI ()
		{
				GUI.Label (new Rect ((screenWidth / 2 - 50), 50, 80, 30)
		           , LanguageManager.GetText ("GameOverScreenMessage")
		           , gameOverStyle);
				GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
		           , LanguageManager.GetText ("Score") + score
		           , gameOverStyle);


				GUIStyle customButton = new GUIStyle ("button");
				customButton.fontSize = screenHeight / 13;

				if (GUI.Button (new Rect ((screenWidth / 2 - (buttonWidth / 2)), 2 * screenHeight / 4, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("Retry")
		                , customButton)) {
						Application.LoadLevel ("main");
				}

				if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 3 * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , customButton)) {
						Application.LoadLevel ("MainMenu");
				}
		}
	
		void Update ()
		{
				// go to main menu on escape/back button
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Debug.Log ("GameOverScript: Escape key pressed");
						Application.LoadLevel ("MainMenu");
				}
		}	
}