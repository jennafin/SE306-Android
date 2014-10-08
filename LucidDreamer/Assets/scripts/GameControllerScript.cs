﻿using UnityEngine;
using System;

public class GameControllerScript : MonoBehaviour {

	// Where the bottom of the levels will be
	public float minYLevelPosition = -3f;

	// Keep track of how many lives the player has
	private int	lives;
	private int coinsCollected = 0;

	// Main Character
	public Transform alexDreamer;

	// The Prefab level segments that can be chosen from
	public GameObject[] levelSegments;

	// Current Theme
	Theme currentTheme = Theme.Bedroom;

	// The number of level segments for this theme
	int currentThemeSegmentCount = 0;

	// Prefabs that the player run play on.
	Level currentLevel;
	Level previousLevel;

	// Use this for initialization
	void Start () {
		// Calculate the screen width


		// Player starts with 3 lives
		lives = 3;

		// TODO: Load bedroom scene

		// Below here is temp stuff until there is a bedroom scene
		this.previousLevel = GetNextLevel (new Vector3(0f, 0f, 0f), Quaternion.identity);
		this.currentLevel = GetNextLevel (new Vector3 (previousLevel.MaxX (), 0f, 0f), Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
		Vector3 alexPosition = alexDreamer.position;
		print ("Alex: " + alexPosition);
		print ("Min: " + currentLevel.MinX ());
		print ("Max: " + currentLevel.MaxX ());

		float tmpPos = Camera.main.WorldToScreenPoint (new Vector3(previousLevel.MaxX(), 0, 0)).x;
		if (tmpPos < 0) {

			Vector3 levelSpawnPostion = new Vector3(currentLevel.MaxX (), 0, 0);
			
			if (previousLevel != null) {
				Destroy(previousLevel.Prefab());
			}
			previousLevel = currentLevel;
			currentLevel = GetNextLevel(levelSpawnPostion, Quaternion.identity);
		}


//		if (alexPosition.x - currentLevel.MinX() > currentLevel.Width() * 0.8) {
//
//			Vector3 levelSegmentSpawnPosition = alexPosition;
//			levelSegmentSpawnPosition.x += currentLevel.MaxX(); // TODO: Figure out
//			levelSegmentSpawnPosition.y = -3f; // Ground position
//
//			if (previousLevel != null) {
//				Destroy(previousLevel.Prefab());
//			}
//			previousLevel = currentLevel;
//			currentLevel = GetNextLevel(levelSegmentSpawnPosition, Quaternion.identity);
//		}
	}

	// Uses the LevelFactory to create the next level segment
	Level GetNextLevel (Vector3 position, Quaternion rotation) {
		LevelFactory factory = new LevelFactory ();
		factory.setTheme (GetNextTheme ());
		factory.setLevelSegment (GetNextPrefab ());
		factory.setPosition (position);
		factory.setRotation (rotation);

		return factory.build ();
	}

	// Returns the theme for the next level segment
	Theme GetNextTheme () {
		if (currentThemeSegmentCount >= 5) {
			currentThemeSegmentCount = 0;
			currentTheme = GetNewTheme();
		} 

		return currentTheme;
	}

	// Chooses and returns a new theme. The returned theme will be different from the current theme.
	Theme GetNewTheme () {
		Array themes = Enum.GetValues(typeof(Theme));
		System.Random random = new System.Random ();
		Theme nextTheme;
		do {
			nextTheme = (Theme) themes.GetValue (random.Next (themes.Length));
		} while (nextTheme != currentTheme);
		return nextTheme;
	}

	// Chooses and returns a new level segment.
	GameObject GetNextPrefab () {
		System.Random random = new System.Random ();
		return levelSegments[random.Next (levelSegments.Length)];
	}

	public void characterCollisionWith(Collision col) {
		if (col.gameObject.tag == "Dangerous") { //TODO this is the incorrect check, currently there are not enemies in this branch
			lives--;
		}
		if (lives < 0) {
			// Game over, TODO move to game over screen
		}
	}

	// Increments the number of collected coins by the specified amount
	public void IncrementCoins(int amount) {
		this.coinsCollected += amount;
		Debug.Log ("GameController: Incremented coins by " + amount + ". Now have: " + this.coinsCollected, this);
	}
}