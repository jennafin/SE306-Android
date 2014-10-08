using UnityEngine;
using System.Collections;
using System.Linq;



public class LifeHUDScript : MonoBehaviour {
	private string lives = "x x x";
	void OnGUI() {
		GUI.TextArea(new Rect(20, 20, 50, 50), lives);
	}

	public void SetLives(int lives) {
		if (lives >= 0) {
			this.lives = string.Concat(Enumerable.Repeat("x ", lives).ToArray()).Trim();
		}
	}
}
