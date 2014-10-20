using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()]  
public class OptionsScript : MonoBehaviour {

	public GUIStyle buttonStyle;
	public GUIStyle toggleStyle;
	public GUIStyle titleTextStyle;
	public GUIStyle labelTextStyle;
	public Language language = Language.English;

	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private int padding;
	private int toggleHeight;
	private int toggleWidth;
	private bool musicOption;
	private bool soundEffectsOption;
	private const string  MUSIC_OPTION_KEY = "MusicOption";
	private const string  SOUND_EFFECTS_OPTION_KEY = "SoundEffectsOption";

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("OptionsScript: Start");
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
		titleTextStyle.fontSize = (int)(0.16 * screenHeight);
		titleTextStyle.alignment = TextAnchor.MiddleCenter;
		labelTextStyle.fontSize = screenHeight / 13;
		labelTextStyle.alignment = TextAnchor.UpperLeft;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = screenHeight / 13;
		buttonWidth = screenWidth / 5;
		buttonHeight = screenHeight / 10;
		padding = screenWidth / 8;

		RetrieveSettings ();

	}

	void OnGUI() 
	{
		GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
		           , LanguageManager.GetText ("Options")
		           , titleTextStyle);

		GUILayout.BeginArea (new Rect (screenWidth / 2 - screenWidth / 4, 3 * screenHeight / 10, screenWidth, screenHeight));
		GUILayout.BeginVertical ();

			GUILayout.BeginHorizontal ();
				GUILayout.BeginArea(new Rect(0,0, buttonWidth, buttonHeight));
				GUI.Label (new Rect (0, 0, 0, 0)
		        		  , LanguageManager.GetText ("Music")
				          , labelTextStyle);
				GUILayout.EndArea ();

				GUILayout.BeginArea (new Rect(buttonWidth * 2,0, buttonWidth/2, buttonHeight));
				musicOption = GUI.Toggle(new Rect(0, 0, buttonWidth/2 ,buttonHeight), musicOption, "", toggleStyle);
				GUILayout.EndArea ();
			GUILayout.EndHorizontal ();

			GUILayout.Space (500);

			GUILayout.BeginHorizontal ();
				GUILayout.BeginArea(new Rect(0, buttonHeight*2, buttonWidth*2, buttonHeight));
				GUI.Label (new Rect (0, 0, 0, 0)
		           		   , LanguageManager.GetText ("SoundEffects")
				           , labelTextStyle);
				GUILayout.EndArea ();
				
				GUILayout.BeginArea (new Rect(buttonWidth * 2,buttonHeight*2, buttonWidth/2, buttonHeight));
				soundEffectsOption = GUI.Toggle(new Rect(0, 0, buttonWidth/2 ,buttonHeight), soundEffectsOption, "", toggleStyle);
				GUILayout.EndArea ();
			GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
		GUILayout.EndArea ();


		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 2.7f * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("SelectLanguage")
		                , buttonStyle)) {
			LoadLanguageMenu();
		}
			
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 3.5f * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , buttonStyle)) {

			LoadMainMenu();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// go to main menu on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("OptionsScript: Escape key pressed");
			LoadMainMenu();
		}
	}

	// Saves options to PlayerPrefs
	private void persistOptions() {
		PlayerPrefs.SetInt (MUSIC_OPTION_KEY, musicOption ? 1 : 0);
		PlayerPrefs.SetInt (SOUND_EFFECTS_OPTION_KEY, soundEffectsOption ? 1 : 0);
	}

	// Persists options on screen and loads Main Menu scene
	private void LoadMainMenu() {
		persistOptions ();
		GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("MainMenu");
	}

	// Persists options on screen and loads the Select Language scene
	private void LoadLanguageMenu() {
		persistOptions ();
		GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("SelectLanguage");
	}

	private void RetrieveSettings() {
		if (PlayerPrefs.HasKey (MUSIC_OPTION_KEY)) {
			musicOption = PlayerPrefs.GetInt(MUSIC_OPTION_KEY) != 0;
		} else {
			musicOption = true;
		}
		if (PlayerPrefs.HasKey (SOUND_EFFECTS_OPTION_KEY)) {
			soundEffectsOption = PlayerPrefs.GetInt(SOUND_EFFECTS_OPTION_KEY) != 0;
		} else {
			soundEffectsOption = true;
		} 
	}
}
