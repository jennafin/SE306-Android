using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreGUI : MonoBehaviour {

	public GUIStyle gameOverStyle;
	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private HighScoreManager highScores = new HighScoreManager();
	private List<ScoreEntry> topScores = new List<ScoreEntry>();

	void Start ()
	{

		screenHeight = Screen.height;
		screenWidth = Screen.width;
		
		gameOverStyle.fontSize = (int)(0.06 * screenHeight);
		gameOverStyle.alignment = TextAnchor.MiddleCenter;
		buttonWidth = screenWidth / 5;
		buttonHeight = screenHeight / 10;
		highScores.Load ();
		topScores.AddRange (highScores.GetTopTenScores ());

	}

	void OnGUI ()
	{
		GUI.Label (new Rect ((screenWidth / 2 - 50), 50, 80, 30)
		           , "High Scores"
		           , gameOverStyle);

		if (topScores.Count == 0) {
				} else {
						GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
			          		 , "No high scores yet"
			           			, gameOverStyle);
						foreach (ScoreEntry score in topScores) {
								GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
			           , score.name + score.score
			           		, gameOverStyle);
						}
				}
		
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = screenHeight / 13;
		
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
