using UnityEngine;
using System.Collections;
using System.Linq;



public class LifeHUDScript : MonoBehaviour {
	private string lives = "x x x";
	public GUIStyle  lifeStyle;
	private int score = 0;

	void OnGUI() {
		lifeStyle.fontSize = (int)(0.06 * Screen.height);
		lifeStyle.normal.textColor = Color.red;
		//Life HUD
		GUI.TextArea(new Rect(30, 20, 50, 50), lives, lifeStyle);

		//ScoreHUD
		GUI.TextArea(new Rect(Screen.width-200, 20, 50, 20), score.ToString(), lifeStyle);


	}

	public void SetLives(int lives) {
		if (lives >= 0) {
			this.lives = string.Concat(Enumerable.Repeat("x ", lives).ToArray()).Trim();
		}
	}

	public void SetScore(int score)
	{
		this.score = score;
	}
}
