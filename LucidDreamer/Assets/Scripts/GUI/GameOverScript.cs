﻿using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{

		public GUIStyle gameOverStyle;
		private int score = 0;
		private int screenHeight;
		private int screenWidth;
		private int buttonWidth;
		private int buttonHeight;
		private string userName = "name";
		private HighScoreManager highScores = new HighScoreManager();
	
		void Start ()
		{
				score = PlayerPrefs.GetInt ("Score");

				screenHeight = Screen.height;
				screenWidth = Screen.width;

				gameOverStyle.fontSize = (int)(0.06 * screenHeight);
				gameOverStyle.alignment = TextAnchor.MiddleCenter;
				buttonWidth = screenWidth / 5;
				buttonHeight = screenHeight / 10;
		}
	
		void OnGUI ()
		{
				GUI.Label (new Rect ((screenWidth / 2 - 50), 50, 80, 30)
		           , "Alex woke up!"
		           , gameOverStyle);
				GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
		           , "Score: " + score
		           , gameOverStyle);


				GUIStyle customButton = new GUIStyle ("button");
				customButton.fontSize = screenHeight / 13;

				if (GUI.Button (new Rect ((screenWidth / 2 - (buttonWidth / 2)), 1.5f * screenHeight / 4, buttonWidth, buttonHeight)
		                , "Retry?"
		                , customButton)) {
						Application.LoadLevel ("main");
				}

				userName = GUI.TextField(new Rect ((screenWidth / 2 - (buttonWidth / 2)), 2 * screenHeight / 4, buttonWidth, buttonHeight)
				              , userName, customButton);

				if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 2.5f * screenHeight / 4, buttonWidth * 2, buttonHeight)
				                , "Save Score"
				                , customButton)) {
					//Save the score
					highScores.Load();
					highScores.AddScore(userName, score);
					highScores.SaveScores();
					Application.LoadLevel ("MainMenu");
				}

				if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 3 * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , "Exit to Menu"
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