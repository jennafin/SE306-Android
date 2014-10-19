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

	void OnGUI() 
	{
		GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
		           , LanguageManager.GetText ("Options")
		           , titleTextStyle);

//		musicOption = GUI.Toggle(new Rect(0, 0, 50,25), musicOption, "", toggleStyle);

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
			Application.LoadLevel ("SelectLanguage");
		}
			
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 3.5f * screenHeight / 4, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , buttonStyle)) {

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

	private void LoadMainMenu() {
		GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("MainMenu");
	}
}
