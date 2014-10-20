using UnityEngine;
using System.Collections;

public class HUDLucidGUI : MonoBehaviour {

	private float barDisplay  = 0;
	private bool usedPower = false;
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	private GUIStyle guiStyle = new GUIStyle();

	void Start(){

		guiStyle.fixedHeight = 0;
		guiStyle.fixedWidth = 0;
		guiStyle.stretchWidth = true;
		guiStyle.stretchHeight = true;

		}

	/* This method is written with help from http://answers.unity3d.com/questions/11892/how-would-you-make-an-energy-bar-loading-progress.html*/
	void OnGUI()
	{

		// draw the background:
		GUI.BeginGroup (new Rect (0, Screen.height-Screen.height/16, Screen.width, Screen.height/16));
		GUI.Box (new Rect (0,0, Screen.width, Screen.height/16),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, Screen.width * barDisplay, Screen.height/16));
		GUI.Box (new Rect (0,0, Screen.width, Screen.height/16),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
	} 
	
	public void UpdateDisplay(float ammount)
	{
		barDisplay = ammount;
	}

}
