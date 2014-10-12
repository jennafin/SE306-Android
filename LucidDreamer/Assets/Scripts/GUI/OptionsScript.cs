using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {

	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private GUIStyle optionsStyle;

	// Use this for initialization
	void Start () 
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;
	}

	void OnGUI() 
	{
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = screenHeight / 13;
		
		if (GUI.Button (new Rect ((screenWidth / 2 - (buttonWidth / 2)), 2 * screenHeight / 4, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("SelectLanguage")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}
			
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 3 * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , customButton)) {
			Application.LoadLevel ("MainMenu");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// go to main menu on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("OptionsScript: Escape key pressed");
			Application.LoadLevel ("MainMenu");
		}
	}
}
