using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public Texture2D fadeOutTexture;		// texture to overlay screen
	public float fadeSpeed = 0.8f;			// fade speed

	private int drawDepth = -1000;			// texture's drawing order in hierarchy - lower number renders to top
	private float alpha = 1.0f;				// texture's alpha
	private int fadeDir = -1;				// direction to fade: in = -1, out = 1

	void OnGUI() {
		// fade out/in alpha based on direction, speed and time
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// clamp alpha between 0 and 1
		alpha = Mathf.Clamp01 (alpha);

		// set color of GUI
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha); 			// set alpha
		GUI.depth = drawDepth;															// set black texture on top
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture); // draw texture to fit entire screen
	}


	// sets fadeDir to the direction parameter making the scene fade in (-1) or out (1)
	public float BeginFade (int direction) {
		fadeDir = direction;
		// return fadeSpeed to time aid in timing the caller's scene load
		return fadeSpeed;
	}

	// Called when scene is loaded. Carries out a fade out 
	void OnLevelWasLoaded() {
		BeginFade (-1);
	}
}
