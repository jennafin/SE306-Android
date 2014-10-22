using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode()] 
public class GUIVoiceSelector : MonoBehaviour {

	public Language language = Language.English;
	public GUIStyle selectLanguageStyle;
	public GUIStyle exitButtonStyle;
	public GUIStyle titleTextStyle;
	

	// Use this for initialization
	void Start () 
	{
		

	}
	
	void OnGUI() 
	{

		if (GUI.Button (new Rect(Screen.width/2 +10,Screen.height/2, Screen.width/4, Screen.height/10),"Hugo") ){
			PlayerPrefs.SetString("AudioName", "Hugo");
		}
		if (GUI.Button (new Rect(Screen.width/2 - Screen.width/4-10,Screen.height/2, Screen.width/4, Screen.height/10),"Jamie")) {
			PlayerPrefs.SetString("AudioName", "Jamie");
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
