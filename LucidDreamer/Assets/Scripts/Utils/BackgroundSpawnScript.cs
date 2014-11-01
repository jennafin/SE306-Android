using UnityEngine;
using System.Collections;

public class BackgroundSpawnScript : MonoBehaviour {
	
	public GameObject defaultBackground;
	public GameObject[] mathBackgrounds;
	public GameObject[] artBackgrounds;
	public GameObject[] scienceBackgrounds;
	
	public GameControllerScript gameController;
	
	GameObject[,] currentImages = new GameObject[3,3];
	
	float imageWidth;
	float imageHeight;
	
	void Start () {
		// Get level width/height
		imageWidth = defaultBackground.renderer.bounds.size.x;
		imageHeight = defaultBackground.renderer.bounds.size.z;
		
		// Create the first 9 screens
		InstantiateLevels();
	}
	
	void FixedUpdate () {
		if (RightIsVisible() && !LeftIsVisible()) {
			MoveRight();
		}
		
		if (UpIsVisible() && !DownIsVisible()) {
			MoveUp ();
		} else if (DownIsVisible() && !UpIsVisible()) {
			MoveDown ();
		}
	}
	
	void MoveRight() {
		for (int i = 0; i <= 2; i++) {
			Destroy (currentImages[0, i]);
			currentImages[0, i] = currentImages[1, i];
			currentImages[1, i] = currentImages[2, i];
			float xPos = currentImages[2, i].transform.position.x + imageWidth;
			float yPos = currentImages[2, i].transform.position.y;
			currentImages[2, i] = InstantiateBackgroundAtPosition(xPos, yPos);
		}
	}
	
	void MoveUp() {
		for (int i = 0; i <= 2; i++) {
			Destroy (currentImages[i, 2]);
			currentImages[i, 2] = currentImages[i, 1];
			currentImages[i, 1] = currentImages[i, 0];
			float xPos = currentImages[i, 0].transform.position.x;
			float yPos = currentImages[i, 0].transform.position.y + imageHeight;
			currentImages[i, 0] = InstantiateBackgroundAtPosition(xPos, yPos);
		}
	}
	
	void MoveDown() {
		for (int i = 0; i <= 2; i++) {
			Destroy (currentImages[i, 0]);
			currentImages[i, 0] = currentImages[i, 1];
			currentImages[i, 1] = currentImages[i, 2];
			float xPos = currentImages[i, 2].transform.position.x;
			float yPos = currentImages[i, 2].transform.position.y - imageHeight;
			currentImages[i, 2] = InstantiateBackgroundAtPosition(xPos, yPos);
		}
	}
	
	bool RightIsVisible() {
		return currentImages[2, 0].renderer.isVisible
				|| currentImages[2, 1].renderer.isVisible
				|| currentImages[2, 2].renderer.isVisible;
	}
	
	bool LeftIsVisible() {
		return currentImages[0, 0].renderer.isVisible
			    || currentImages[0, 1].renderer.isVisible
				|| currentImages[0, 2].renderer.isVisible;
	}
	
	bool UpIsVisible() {
		return currentImages[0, 0].renderer.isVisible
				|| currentImages[1, 0].renderer.isVisible
				|| currentImages[2, 0].renderer.isVisible;
	}
	
	bool DownIsVisible() {
		return currentImages[0, 2].renderer.isVisible
				|| currentImages[1, 2].renderer.isVisible
				|| currentImages[2, 2].renderer.isVisible;
	}
	
	void InstantiateLevels() {
		for (int y = -1; y <= 1; y++) {
			for (int x = -1; x <= 1; x++) {
				float xPos = this.transform.position.x + x * imageWidth - imageWidth / 2;
				float yPos = this.transform.position.y - y * imageHeight + imageHeight / 2;
				currentImages[x + 1, y + 1] = InstantiateBackgroundAtPosition(xPos, yPos);
			}
		}
		
	}
	
	GameObject InstantiateBackgroundAtPosition(float xPos, float yPos) {
		return Instantiate (ChooseBackgroundImage(), new Vector3(xPos, yPos, 0), Quaternion.AngleAxis(270, Vector3.right)) as GameObject;
	}
	
	GameObject ChooseBackgroundImage() {
		Theme theme = gameController.GetCurrentTheme();
		switch (theme) {
			case Theme.Art:
				return ChooseBackgroundImage(artBackgrounds);
				break;
			case Theme.Maths:
				return ChooseBackgroundImage(mathBackgrounds);
				break;
			case Theme.Science:
				return ChooseBackgroundImage(scienceBackgrounds);
				break;
		}
		return defaultBackground;
		
	}
	
	GameObject ChooseBackgroundImage(GameObject[] backgroundImages) {
		System.Random random = new System.Random ();
		try {
			return backgroundImages[random.Next(backgroundImages.Length)];
		} catch {
			return defaultBackground;
		}
	}
}