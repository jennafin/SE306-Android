using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()] 
public class SelectLanguageScript : MonoBehaviour {

	public Language language = Language.English;
	public GUIStyle selectLanguageStyle;
	public GUIStyle exitButtonStyle;
	public GUIStyle titleTextStyle;

	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;

	// Use this for initialization
	void Start () 
	{
		
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
		
		selectLanguageStyle.fontSize = screenHeight / 18;
		selectLanguageStyle.alignment = TextAnchor.MiddleCenter;

		exitButtonStyle.fontSize = screenHeight / 13;
		exitButtonStyle.alignment = TextAnchor.MiddleCenter;

		titleTextStyle.fontSize = (int)(0.16 * screenHeight);
		titleTextStyle.alignment = TextAnchor.MiddleCenter;

		buttonWidth = 4 * screenWidth / 10;
		buttonHeight = screenHeight / 10;
	}

	void OnGUI() 
	{
		GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
		           , LanguageManager.GetText ("SelectLanguage")
		           , titleTextStyle);

		GUILayout.BeginArea (new Rect (screenWidth / 10, 3.0f * screenHeight / 10, 8* screenWidth/10, screenHeight));
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (LanguageManager.GetText ("english")
			                , selectLanguageStyle
		                    , GUILayout.Width(buttonWidth - 10)
		                    , GUILayout.Height(buttonHeight))) {
				ChangeLanguage (Language.English);
			}
		GUILayout.Space (20);
		if (GUILayout.Button (LanguageManager.GetText ("spanish")
		                	  , selectLanguageStyle
		                      , GUILayout.Height(buttonHeight))) {
			ChangeLanguage (Language.Spanish);
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (buttonHeight/3);
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (LanguageManager.GetText ("chinese")
		                      , selectLanguageStyle
		                      , GUILayout.Width(buttonWidth - 10)
		                      , GUILayout.Height(buttonHeight))) {
			ChangeLanguage (Language.Chinese);
		}
		GUILayout.Space (20);
		if (GUILayout.Button (LanguageManager.GetText ("french")
		                      , selectLanguageStyle
		                      , GUILayout.Height(buttonHeight))) {
			ChangeLanguage (Language.French);
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (buttonHeight/3);
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (LanguageManager.GetText ("russian")
		                      , selectLanguageStyle
		                      , GUILayout.Width(buttonWidth - 10)
		                      , GUILayout.Height(buttonHeight))) {
			ChangeLanguage (Language.Russian);
		}
		GUILayout.Space (20);
		if (GUILayout.Button (LanguageManager.GetText ("italian")
		                      , selectLanguageStyle
		                      , GUILayout.Height(buttonHeight))) {
			ChangeLanguage (Language.Italian);
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (buttonHeight/3);
		GUILayout.BeginHorizontal ();


		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth/2, 7 * screenHeight / 8, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("Back")
		                , exitButtonStyle)) {
			LoadOptionsMenu();
		}
	}

	void ChangeLanguage(Language l)
	{
		language = l;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream f = File.Open(Application.persistentDataPath + "/language.dat", FileMode.OpenOrCreate);
		bf.Serialize(f, l);
		f.Close();
		LoadOptionsMenu ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// go to options menu on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("SelectLanguageScript: Escape key pressed");
			LoadOptionsMenu();;
		}
	}

	// Loads the options menu scene using the SceneFader script
	private void LoadOptionsMenu() {
		GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("Options");
	}
}
