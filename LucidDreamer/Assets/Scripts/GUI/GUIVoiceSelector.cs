using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()] 
public class GUIVoiceSelector : MonoBehaviour {

	public Language language = Language.English;
	public GUIStyle buttonStyle;
	public GUIStyle selectLanguageStyle;
	public GUIStyle exitButtonStyle;
	public GUIStyle titleTextStyle;
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
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = Screen.height / 13;
		titleTextStyle.fontSize = (int)(0.16 * Screen.height);
		titleTextStyle.alignment = TextAnchor.MiddleCenter;
		buttonWidth = Screen.width / 5;
		buttonHeight = Screen.height / 10;

	}
	
	void OnGUI() 
	{

		GUI.Label (new Rect (0, Screen.height / 10, Screen.width, 0)
		           , LanguageManager.GetText ("SelectVoice")
		           , titleTextStyle);

		if (GUI.Button (new Rect(Screen.width/2 +10,Screen.height/2, Screen.width/4, Screen.height/10),"Hugo", buttonStyle) ){
			PlayerPrefs.SetString("AudioName", "Hugo");
		}
		if (GUI.Button (new Rect(Screen.width/2 - Screen.width/4-10,Screen.height/2, Screen.width/4, Screen.height/10),"Jamie", buttonStyle)) {
			PlayerPrefs.SetString("AudioName", "Jamie");
		}

		if (GUI.Button (new Rect(Screen.width/2 - Screen.width/8,Screen.height/2 + Screen.height/10*1.5f, Screen.width/4, Screen.height/10),"Nick", buttonStyle)) {
			PlayerPrefs.SetString("AudioName", "Nick");
		}


		if (GUI.Button (new Rect (Screen.width / 2 - buttonWidth/2, 7 * Screen.height / 8, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("Back")
		                , buttonStyle)) {
			//LoadOptionsMenu();
			GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("Options");
		}
		

	}
	
	void ChangeVoice(Language l)
	{
		language = l;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream f = File.Open(Application.persistentDataPath + "/voice.dat", FileMode.OpenOrCreate);
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
