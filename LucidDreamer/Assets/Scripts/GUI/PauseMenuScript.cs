using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()]
public class PauseMenuScript : MonoBehaviour {

	public GUIStyle buttonStyle;
	public GUIStyle titleTextStyle;
	public Texture fadeOverlayTexture;
	public float fadeSpeed = 0.8f;
	public Language language = Language.English;
	private bool isPaused = false;
	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private int drawDepth = -1000;			// texture's drawing order in hierarchy - lower number renders to top
	private float alpha = 1.0f;				// texture's alpha
	private int fadeDir = -1;				// direction to fade: in = -1, out = 1

	// Use this for initialization
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

			GUILayout.BeginArea (new Rect (screenWidth / 2 - screenWidth / 4, 5.5f * screenHeight / 13, screenWidth / 2, screenHeight));
			GUILayout.BeginVertical ();
			if (GUILayout.Button (LanguageManager.GetText ("Resume"), buttonStyle)) {
				isPaused = false;
				FadeOut();
				GameObject.Find ("GameController").GetComponent<GameControllerScript> ().UnpauseGame();

			}

			GUILayout.Space (buttonHeight);

			if (GUILayout.Button (LanguageManager.GetText ("ExitToMenu"), buttonStyle)) {
				GameObject.Find ("GameController").GetComponent<GameControllerScript> ().UnpauseGame();
				LoadMainMenu ();
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();

		} else {
			FadeOut();
			if (GUI.Button(new Rect(screenWidth - (buttonWidth/4), buttonHeight, buttonWidth/4, buttonHeight), "")) {
				isPaused = true;
				FadeIn();
				GameObject.Find ("GameController").GetComponent<GameControllerScript> ().PauseGame();
			}
		}

//		// fade out/in alpha based on direction, speed and time
//		alpha += fadeDir * fadeSpeed * Time.deltaTime;
//		// clamp alpha between 0 and .5
//		alpha = Mathf.Clamp (alpha, 0f, 0.5f); 


		// set color of GUI
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha); 			// set alpha
		GUI.depth = drawDepth;															// set black texture on top
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOverlayTexture); // draw texture to fit entire screen
			
	}

	private void LoadMainMenu() {
		GameObject.Find ("GameController").GetComponent<SceneFader> ().LoadScene("MainMenu");
	}

	private void FadeIn() {
		alpha = 0.3f;
//		fadeDir = 1;
	}

	private void FadeOut() {
		alpha = 0.0f;
//		fadeDir = -1;
	}
}
