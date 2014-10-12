using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SelectLanguageScript : MonoBehaviour {

	public Language language = Language.English;
	public GUIStyle selectLanguageStyle;
	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("SelectLanguageScript: Start");
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

		selectLanguageStyle = new GUIStyle ();
		selectLanguageStyle.fontSize = (int)(0.06 * screenHeight);
		selectLanguageStyle.alignment = TextAnchor.MiddleCenter;

		buttonWidth = screenWidth / 5;
		buttonHeight = screenHeight / 10;
	}

	void OnGUI() 
	{
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = screenHeight / 13;
		
		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("english")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}

		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 2 * screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("spanish")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}

		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 3 * screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("french")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}

		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 4 * screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("russian")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}

		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 5 * screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("italian")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}

		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 6 * screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("chinese")
		                , customButton)) {
			Application.LoadLevel ("SelectLanguage");
		}
		
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth, 7 * screenHeight / 8, buttonWidth * 2, buttonHeight)
		                , LanguageManager.GetText ("ExitToMenu")
		                , customButton)) {
			Application.LoadLevel ("MainMenu");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// go to options menu on escape/back button
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("SelectLanguageScript: Escape key pressed");
			Application.LoadLevel ("Options");
		}
	}
}
