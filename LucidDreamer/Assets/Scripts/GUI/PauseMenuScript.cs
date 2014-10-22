using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()]
public class PauseMenuScript : MonoBehaviour {

	public GUIStyle buttonStyle;
	public GUIStyle titleTextStyle;
	public GUIStyle pausedButtonStyle;
	public Texture fadeOverlayTexture;				// texture for the pause menu's background
	public Language language = Language.English;
	
	private bool isPaused = false;			
	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private int drawDepth = -1000;			// overlay's drawing order in hierarchy - lower number renders to top
	private float alpha = 1.0f;				// alpha of pause overlay

	void Start () {
		// Initialise language file
		// If not blank then load it
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

		screenHeight = Screen.height;
		screenWidth = Screen.width;

		buttonStyle.fontSize =  screenHeight / 13;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		pausedButtonStyle.fontSize = screenHeight / 25;
		pausedButtonStyle.alignment = TextAnchor.MiddleCenter;
		titleTextStyle.fontSize = (int)(0.16 * screenHeight);
		titleTextStyle.alignment = TextAnchor.MiddleCenter;

		buttonWidth = screenWidth / 5;
		buttonHeight = screenHeight / 10;
	}

	void OnGUI() {
		if (isPaused) {

			GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
	           			, LanguageManager.GetText ("Paused")
	           			, titleTextStyle);

			GUILayout.BeginArea (new Rect (screenWidth / 2 - screenWidth / 4
											, 4f * screenHeight / 13
											, screenWidth / 2
											, screenHeight));
			GUILayout.BeginVertical ();
			if (GUILayout.Button (LanguageManager.GetText ("Resume")
									, buttonStyle
									, GUILayout.Height(buttonHeight))) {
				UnpauseGame();
			}

			GUILayout.Space (buttonHeight);
			
			if (GUILayout.Button (LanguageManager.GetText ("Restart")
			                      , buttonStyle
			                      , GUILayout.Height(buttonHeight))) {
			    UnpauseGame();
				LoadGame ();
			}
			
			GUILayout.Space (buttonHeight);
			
			if (GUILayout.Button (LanguageManager.GetText ("ExitToMenu")
									, buttonStyle
									, GUILayout.Height(buttonHeight))) {
				UnpauseGame();
				LoadMainMenu ();
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();

		} else {
			FadeOut();
			if (GUI.Button(new Rect(screenWidth - (buttonWidth/3)
			                        	, buttonHeight/4 - buttonHeight/10
										, buttonWidth/4
										, buttonHeight)
										, " ▌▌"
										, pausedButtonStyle)) {
				PauseGame();
			}
		}

		// set faded overlay for pause menu
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha); 				// set alpha
		GUI.depth = drawDepth;																// set black texture on top
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOverlayTexture); // draw texture to fit entire screen
			
	}
	
	void Update() {
		// toggle pause menu on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused) {
				UnpauseGame();
			} else {
				PauseGame();
			}
		}
	}

	// loads the MainMenu scene using the SceneFade script attached to the GameController GameObject
	private void LoadMainMenu() {
		Application.LoadLevel("MainMenu");
	}

	// loads the main (the game) scene using the SceneFade script attached to the GameController GameObject
	private void LoadGame() {
		Application.LoadLevel("main");
	}
	// sets the alpha value of the pause overlay texture to be visible
	private void FadeIn() {
		alpha = 0.3f;
	}

	// sets the alpha value of the pause overlay to zero
	private void FadeOut() {
		alpha = 0.0f;
	}
	
	// set boolean to pause the game and inform the Game Controller of this action	
	private void PauseGame() {
		isPaused = true;
		FadeIn();
		GameObject.Find ("GameController").GetComponent<GameControllerScript> ().PauseGame();
	}
	
	// set boolean to unpause the game and inform the Game Controller of this action
	private void UnpauseGame() {
		isPaused = false;
		FadeOut();
		GameObject.Find ("GameController").GetComponent<GameControllerScript> ().UnpauseGame();
	}
}
