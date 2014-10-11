using UnityEngine;
using System.Collections;
using System.Linq;
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
						GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
						           , "No high scores yet"
						           , gameOverStyle);
				} else {
						
						int height = 0;
						int i = 1;
						foreach (ScoreEntry score in Enumerable.Reverse(topScores)) {
								GUI.Label (new Rect((screenWidth / 4), (screenHeight / 4)+height, 80, 30), i.ToString(), gameOverStyle);
								GUI.Label (new Rect ((screenWidth / 2 - 90), (screenHeight / 4)+height, 80, 30)
			           				, score.name
			           				, gameOverStyle);
								GUI.Label (new Rect ((screenWidth / 2 + 30), (screenHeight / 4)+height, 80, 30)
								           , score.score.ToString()
								           , gameOverStyle);
								height = height + screenHeight/15;
								i = i + 1;
						}
				}
		
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = screenHeight / 13;
		
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, screenHeight - buttonHeight - 20, buttonWidth * 2, buttonHeight)
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
