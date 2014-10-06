using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	
	int score = 0;
	private bool retryButton;
	private bool exitToMenuButton;

	// Use this for initialization
	void Start () {
		//score = PlayerPrefs.GetInt ("Score");
	}
	
	void OnGUI() {
		GUI.Label (new Rect (Screen.width / 2 - 40, 50, 80, 30), "GAME OVER");
		
		GUI.Label (new Rect(Screen.width /2 - 40, 300, 80, 30), "Score: " + score);
		
		retryButton = GUI.Button(new Rect(Screen.width / 2 - 30, 350, 60, 30), "Retry?");

		exitToMenuButton = GUI.Button(new Rect(Screen.width / 2 - 50, 400, 100, 30), "Exit to Menu");
	}
	
	void Update() {
		if (retryButton) {
			Application.LoadLevel ("main");
		} else if (exitToMenuButton) {
			Application.LoadLevel ("MainMenu");
		}

		// go to main menu on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log("GameOverScript: Escape key pressed");
			Application.LoadLevel("MainMenu");
		}
	}
	
	
}