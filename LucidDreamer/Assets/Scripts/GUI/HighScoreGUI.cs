using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()]  
public class HighScoreGUI : MonoBehaviour {

	public GUIStyle leftAlignedStyle;
	public GUIStyle rightAlignedStyle;
	public GUIStyle titleTextStyle;
	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private HighScoreManager highScores = new HighScoreManager();
	private List<ScoreEntry> topScores = new List<ScoreEntry>();
	public Language language = Language.English;

	void Start ()
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;

		titleTextStyle.fontSize = (int)(0.16 * screenHeight);
		leftAlignedStyle.fontSize = (int)(0.04 * screenHeight);
		rightAlignedStyle.fontSize = (int)(0.04 * screenHeight);
		titleTextStyle.alignment = TextAnchor.MiddleCenter;
		leftAlignedStyle.alignment = TextAnchor.MiddleLeft;
		rightAlignedStyle.alignment = TextAnchor.MiddleRight;
		buttonWidth = screenWidth / 5;
		buttonHeight = screenHeight / 10;
		highScores.Load ();
		topScores.AddRange (highScores.GetTopTenScores ());

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

	}

	void OnGUI ()
	{
		GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
		           , LanguageManager.GetText ("HighScores")
		           , titleTextStyle);

		if (topScores.Count == 0) {
						GUI.Label (new Rect ((screenWidth / 2 - 50), screenHeight / 4, 80, 30)
						           , LanguageManager.GetText ("NoHighScores")
						           , leftAlignedStyle);
				} else {
						int minHeight = (int)(screenHeight / 4.5);
						int deltaHeight = 0;
						int verticalPadding = 0;
						int horizontalPadding = screenWidth / 10;
						int i = 1;
						foreach (ScoreEntry score in topScores) {
						GUI.Label (new Rect(horizontalPadding * 2, minHeight + deltaHeight, 0, 0), i.ToString(), rightAlignedStyle);
				GUI.Label (new Rect (horizontalPadding * 2 + 50, minHeight + deltaHeight, 0, 0)
			           				, score.name
			           				, leftAlignedStyle);
				GUI.Label (new Rect (horizontalPadding * 8, minHeight + deltaHeight, 0, 0)
								           , score.score.ToString()
								           , rightAlignedStyle);
						deltaHeight = deltaHeight + screenHeight/15;
						i = i + 1;
						}
				}
		
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = screenHeight / 13;
		
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, screenHeight - buttonHeight - 20, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , customButton)) {
			GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("MainMenu");
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
