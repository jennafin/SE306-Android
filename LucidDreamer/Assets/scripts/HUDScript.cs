using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	public GameControllerScript gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI()
	{
		GUI.TextArea (new Rect(20,20, 50, 20), gameController.GetScore().ToString());
	}
}
