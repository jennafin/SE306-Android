using UnityEngine;
using System.Collections;
using System.Linq;

public class LifeHUDScript : MonoBehaviour {
	public GUIStyle  lifeStyle;
	private int score = 0;
	public Texture heart;
	
	int numberOfLives;
	
	float heartWidth;
	float heartHeight;
	float firstHeartStartX;
	float firstHeartStartY;
	

	void Start() {
		heartWidth = Screen.width * 0.02f;
		heartHeight = Screen.height * 0.03f;
		firstHeartStartX = heartWidth;
		firstHeartStartY = heartHeight;
		
		numberOfLives = 3; // Default
	}
	
	void OnGUI() {
		drawLives ();
		drawScore ();	
	}
	
	void drawScore() {
		lifeStyle.fontSize = (int) Mathf.Ceil(0.05f * Screen.height);
		lifeStyle.normal.textColor = Color.red;
		
		Vector2 size = lifeStyle.CalcSize(new GUIContent(score.ToString()));
		
		
		GUI.TextArea(new Rect(Screen.width - size.x - firstHeartStartX, firstHeartStartY / 2, size.x, size.y), score.ToString(), lifeStyle);
	}
	
	void drawLives() {
		if (! heart) {
			
		} else {
			for (int i = 0; i < numberOfLives; i++) {
				GUI.DrawTexture(new Rect(firstHeartStartX + 1.5f * i * heartWidth, firstHeartStartY, heartWidth, heartHeight), heart, ScaleMode.ScaleAndCrop, true); 
			}
		}
	}

	public void SetLives(int lives) {
		this.numberOfLives = lives;
	}

	public void SetScore(int score)
	{
		this.score = score;
	}
}
