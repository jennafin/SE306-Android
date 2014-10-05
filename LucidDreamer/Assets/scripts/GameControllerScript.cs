using UnityEngine;
using System;

public class GameControllerScript : MonoBehaviour {

	// Keep track of how many lives the player has
	private int	lives;

	//Keep track of scoring
	private ScoreSystem scoreSystem;

	// Main Character
	public GameObject alexDreamer;

	// The Prefab level segments that can be chosen from
	public GameObject[] levelSegments;

	// Current Theme
	Theme currentTheme = Theme.Bedroom;

	// The number of level segments for this theme
	int currentThemeSegmentCount = 0;

	// Prefabs that the player run play on.
	GameObject currentPrefab;
	GameObject previousPrefab;

	// Use this for initialization
	void Start () {
		// Player starts with 3 lives
		lives = 3;

		scoreSystem = new ScoreSystem ();

		// Load bedroom scene
	}

	// Update is called once per frame
	void Update () {
		if (true) {
			// Just while some shit hasn't been done.
			return;
		}

		Vector3 alexPosition = alexDreamer.renderer.bounds.center;

		Bounds currentBounds = currentPrefab.renderer.bounds;
		float currentWidth = currentBounds.max.x - currentBounds.min.x;

		if (alexPosition.x > currentBounds.min.x + currentWidth * 0.8) {
			Destroy(previousPrefab);
			previousPrefab = currentPrefab;
			currentPrefab = GetNextLevelSegment();

			// Do the fancy stuff to actually get the next prefab on the screen
			// I think it's instantiate stuff, but level factory stuff needs to be done for that.
		}

		scoreSystem.UpdateScore ((int)Math.Floor(alexPosition.x));
	}

	void Destroy (GameObject gameObject) {
		if (gameObject != null && gameObject.renderer.bounds.max.x < -10) {
			if (gameObject.gameObject.transform.parent) {
				Destroy (gameObject.gameObject.transform.parent.gameObject);
			} else {
				Destroy (gameObject.gameObject);
			}
		}
	}

	public void AddPoints(int points)
	{
		scoreSystem.AddPoints (points);
	}

	public void AddMultiplier(int multi, int time)
	{
		scoreSystem.AddMultiplier (multi, time);
	}

	public void ResetMultiplier()
	{
		scoreSystem.ResetMultiplier ();
	}

	public int GetScore()
	{
		return scoreSystem.GetScore ();
	}

	// Uses the LevelFactory to create the next level segment
	GameObject GetNextLevelSegment () {
		LevelFactory factory = new LevelFactory ();
		factory.setTheme (GetNextTheme ());
		factory.setLevelSegment (GetNextPrefab ());

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
				if (col.gameObject.name == "enemy") { //TODO this is the incorrect check, currently there are not enemies in this branch
						lives--;
				}
				if (lives < 0) {
						// Game over, TODO move to game over screen
				}
	}
}