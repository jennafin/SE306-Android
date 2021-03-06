﻿using UnityEngine;
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
		heartWidth = Screen.width * 0.05f;
		heartHeight = Screen.height * 0.06f;
		firstHeartStartX = heartWidth;
		firstHeartStartY = heartHeight;
		
		numberOfLives = 3; // Default
	}
	
	void OnGUI() {
		drawLives ();
		drawScore ();	
	}
	
	void drawScore() {
		lifeStyle.fontSize = (int) Mathf.Ceil(0.08f * Screen.height);
		
		Vector2 size = lifeStyle.CalcSize(new GUIContent(score.ToString()));
		//
		
		GUI.TextArea(new Rect(Screen.width - size.x - firstHeartStartX*2, firstHeartStartY / 2, size.x, size.y), score.ToString(), lifeStyle);
	}
	
	void drawLives() {
		if (! heart) {
			
		} else {
			for (int i = 0; i < numberOfLives; i++) {
				GUI.DrawTexture(new Rect(firstHeartStartX + 1.1f * i * heartWidth, firstHeartStartY, heartWidth, heartHeight), heart, ScaleMode.ScaleToFit, true); 
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
