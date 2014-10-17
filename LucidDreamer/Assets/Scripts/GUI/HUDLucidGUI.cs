using UnityEngine;
using System.Collections;

public class HUDLucidGUI : MonoBehaviour {

	private float barDisplay  = 0;
	private bool usedPower = false;
	public Vector2 pos  = new Vector2(20,40);
	public Vector2 size = new Vector2(60,20);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;

	/* This method is written with help from http://answers.unity3d.com/questions/11892/how-would-you-make-an-energy-bar-loading-progress.html*/
	void OnGUI()
	{
		
		// draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
	} 
	
	public void UpdateDisplay(float ammount)
	{
		barDisplay = ammount;
	}

}
