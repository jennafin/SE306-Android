using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class OptionsScript : MonoBehaviour {

	private int screenHeight;
	private int screenWidth;
	private int buttonWidth;
	private int buttonHeight;
	private GUIStyle optionsStyle;
	public Language language = Language.English;

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
		optionsStyle = new GUIStyle ();
		optionsStyle.fontSize = (int)(0.06 * screenHeight);
		optionsStyle.alignment = TextAnchor.MiddleCenter;
		buttonWidth = screenWidth / 5;
		buttonHeight = screenHeight / 10;
	}

	void OnGUI() 
	{
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = screenHeight / 13;
		
		if (GUI.Button (new Rect ((screenWidth / 2 - buttonWidth), 2 * screenHeight / 4, buttonWidth * 2, buttonHeight)
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
			
			Application.LoadLevel ("MainMenu");
		}
	}
}
