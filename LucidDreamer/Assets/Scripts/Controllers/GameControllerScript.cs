using UnityEngine;
using System;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour
{
		private double maxTimeScale;
		private double minTimeScale;
		private double timeScaleIncrement;
		private SpeedHandle timeScale;

		// Keep track of how many lives the player has
		private const int MAX_NUMBER_OF_LIVES = 3;
		private int	lives;
		private int coinsCollected = 0;

		// Keep track of the last collision with an enemy
		private int lastCollision = 0;

		//Scoring system
		private ScoreTrackingSystem scoreTracker;
		private Vector3 alexPosition;

		// Life HUD
		public GameObject LifeHUD;
		private AchievementsList achievementsList = new AchievementsList ();


		// Main Character
		public Transform alexDreamer;
		public MainCharacterScript mainCharacterScript;


		// The Prefab level segments that can be chosen from
		public GameObject[] levelSegments;

		// Current Theme
		Theme currentTheme = Theme.Bedroom;

		// The number of level segments for this theme
		int currentThemeSegmentCount = 0;

		// Prefabs that the player run play on.
		Level currentLevel;
		Level previousLevel;

		// Current power-ups (or could be coins)
		private List<Collectable> currentCollectables = new List<Collectable> ();

		// ShakeDetector to increase lucid power
		public GameObject shakeDetector;


		// Use this for initialization
		void Start ()
		{
				minTimeScale = 1.0f;
				maxTimeScale = 2.0f;
				timeScaleIncrement = 0.00005;

				timeScale = new DefaultSpeedHandle (minTimeScale, minTimeScale, maxTimeScale);

				// Player starts with 3 lives
				lives = MAX_NUMBER_OF_LIVES;


				//Instantiate score tracker
				scoreTracker = new ScoreTrackingSystem ();

				// TODO: Load bedroom scene

				// Below here is temp stuff until there is a bedroom scene
				this.previousLevel = GetNextLevel (new Vector3 (0f, 0f, 0f), Quaternion.identity);
				this.currentLevel = GetNextLevel (new Vector3 (previousLevel.MaxX (), 0f, 0f), Quaternion.identity);
		}

		// Update is called once per frame
		void Update ()
		{
				// Handles the game speeding up.
				Time.timeScale = (float) timeScale.getCurrentSpeed ();
				timeScale.incrementSpeed (timeScaleIncrement);

				// exit game on escape/back button
				if (Input.GetKeyDown (KeyCode.Escape)) {

						Application.LoadLevel ("MainMenu");
				}

				ApplyCollectableBehaviours ();


				alexPosition = alexDreamer.position;

				if (alexPosition.y < -5) {
						// Alex has fallen to his death
						GameOver ();
				}

				float tmpPos = Camera.main.WorldToScreenPoint (new Vector3 (previousLevel.MaxX (), 0, 0)).x;
				if (tmpPos < 0) {

						Vector3 levelSpawnPosition = new Vector3 (currentLevel.MaxX (), 0, 0);

						if (previousLevel != null) {
								Destroy (previousLevel.Prefab ());
						}
						previousLevel = currentLevel;
						currentLevel = GetNextLevel (levelSpawnPosition, Quaternion.identity);
				}

				checkAchievements (alexPosition.x);

				LifeHUD.GetComponent<LifeHUDScript> ().SetScore (scoreTracker.GetCurrentScore ((int)Math.Floor (alexPosition.x)));
		}

		void checkAchievements (float x)
		{
				if (x >= 10) {
						achievementsList.GetRan10Meters ();
				}
				if (x >= 20) {
						achievementsList.GetRan20Meters ();
				}
				if (x >= 30) {
						achievementsList.GetRan30Meters ();
				}
				if (x >= 40) {
						achievementsList.GetRan40Meters ();
				}
				if (x >= 50) {
						achievementsList.GetRan50Meters ();
				}

		}

		// Uses the LevelFactory to create the next level segment
		Level GetNextLevel (Vector3 position, Quaternion rotation)
		{
				LevelFactory factory = new LevelFactory ();
				factory.setTheme (GetNextTheme ());
				factory.setLevelSegment (GetNextPrefab ());
				factory.setPosition (position);
				factory.setRotation (rotation);

				return factory.build ();
		}

		// Returns the theme for the next level segment
		Theme GetNextTheme ()
		{
				if (currentThemeSegmentCount >= 5) {
						currentThemeSegmentCount = 0;
						currentTheme = GetNewTheme ();
				}

				return currentTheme;
		}



		// Chooses and returns a new theme. The returned theme will be different from the current theme.
		Theme GetNewTheme ()
		{
				Array themes = Enum.GetValues (typeof(Theme));
				System.Random random = new System.Random ();
				Theme nextTheme;
				do {
						nextTheme = (Theme)themes.GetValue (random.Next (themes.Length));
				} while (nextTheme != currentTheme);
				return nextTheme;
		}

		// Chooses and returns a new level segment.
		GameObject GetNextPrefab ()
		{
				System.Random random = new System.Random ();
				return levelSegments [random.Next (levelSegments.Length)];
		}

		// Duplicate method to allow loss of life with Collider object, should change later
		public void CharacterColliderWith (Collider2D col)
		{
				int delta = 500;

				String objectTag = col.gameObject.tag;
				String objectName = col.gameObject.name;

				// cooldown after being hit, Alex won't be able to lose a life for some amount of secconds after being hit
				if (objectTag == "Dangerous") {
						if (objectName.Contains ("Enemy")) {

								col.gameObject.GetComponent<Enemy> ().OnCollision (this);
						}
						if (this.mainCharacterScript.isInvincible) {
								return;
						}
						int difference = Math.Abs (Environment.TickCount - lastCollision);
						if (difference > delta) {
								lives--;
								lastCollision = Environment.TickCount;
								LifeHUD.GetComponent<LifeHUDScript> ().SetLives (lives);
						}


						// Resets time scale to normal
						timeScale.reset();

						// Plays injured/death sound
						if (lives < 0) {
							mainCharacterScript.PlayDeathSound();
						} else {
							mainCharacterScript.PlayInjuredSound();
						}
				} else if (objectTag.StartsWith ("Collectable")) {

						Collectable collectable = col.gameObject.GetComponent<Collectable> ();
						this.currentCollectables.Add (collectable);

						// We keep the Collectable instance around, but remove its game object from the scene
						collectable.PlayCollectedSound ();
						Destroy (collectable.gameObject);
				}

				if (lives < 0) {
						LoadGameOverScreen (); // Loads game over screen after 1.5 seconds
				}
		}

		public void LoadGameOverScreen() {
			scoreTracker.gameOver ((int)Math.Floor (alexPosition.x));
			Application.LoadLevel ("GameOver");
		}

		public int GetCoinsCollected ()
		{
				return this.coinsCollected;
		}

		public float GetDistance()
		{
			return alexPosition.x;
		}

		void GameOver ()
		{
				scoreTracker.gameOver ((int)Math.Floor (alexPosition.x));
				Application.LoadLevel ("GameOver");
		}

		// Increments the number of collected coins by the specified amount
		public void IncrementCoins (int amount)
		{
				this.coinsCollected += amount;
				//
				scoreTracker.AddPoints (amount);
		}

		public void IncrementLives (int livesToGive)
		{
				this.lives += livesToGive;

				if (this.lives > MAX_NUMBER_OF_LIVES) {
						this.lives = MAX_NUMBER_OF_LIVES;
				}

				LifeHUD.GetComponent<LifeHUDScript> ().SetLives (this.lives);
		}

		public ScoreTrackingSystem GetScoreTrackingSystem ()
		{
				return this.scoreTracker;
		}

		public void SetScoreTrackingSystem (ScoreTrackingSystem sts)
		{
				this.scoreTracker = sts;
		}

		public MainCharacterScript getMainCharacter ()
		{
				return mainCharacterScript;
		}

		// Iterate through any current collectables and apply their behaviours
		private void ApplyCollectableBehaviours ()
		{
				List<Collectable> expiredCollectables = new List<Collectable> ();

				for (int i = 0; i < this.currentCollectables.Count; i++) {
						Collectable collectable = this.currentCollectables [i];
						bool stillHasLife = collectable.UseOneFrame (this);

						if (!stillHasLife) {
								expiredCollectables.Add (collectable);
						}
				}

				// Remove any expired collectables
				for (int i = 0; i < expiredCollectables.Count; i++) {
						this.currentCollectables.Remove (expiredCollectables [i]);
				}
		}

		/**
		 * Add all of the collectables on screen to currentCollectables, then destroy their GameObjects
		 */
		public void CollectAllCollectables ()
		{

			GameObject[] coins = GameObject.FindGameObjectsWithTag("CollectableCoin");
			GameObject[] powerUps = GameObject.FindGameObjectsWithTag("CollectablePowerUp");

			// Combine these collectables into one array
			GameObject[] collectables = new GameObject[coins.Length + powerUps.Length];
			coins.CopyTo(collectables, 0);
			powerUps.CopyTo(collectables, coins.Length);

			for (int i = 0; i < collectables.Length; i++)
			{
				GameObject collectableGameObject = collectables[i].gameObject;
				Collectable collectable = collectableGameObject.GetComponent<Collectable>();

				if (collectableGameObject.renderer.isVisible)
				{
					currentCollectables.Add(collectable);
					Destroy(collectable.gameObject);
				}

			}
		}

		public void AddLucidPower(float power) {
			shakeDetector.GetComponent<ShakeDetectorScript>().AddLucidPower(power);
		}
}
