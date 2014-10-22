using UnityEngine;
using System.Collections;

public class LevelPicker {

	const float EASY_THRESHOLD = 0f;
	const float MEDIUM_THRESHOLD = 750f;
	const float HARD_THRESHOLD = 2000f;

	GameObject[] easy;
	GameObject[] medium;
	GameObject[] hard;
	
	System.Random random = new System.Random();
	
	public LevelPicker(GameObject[] easy, GameObject[] medium, GameObject[] hard) {
		this.easy = easy;
		this.medium = medium;
		this.hard = hard;
	}
	
	public GameObject ChooseLevel(float distanceTravelled) {
		int randomDistance = random.Next((int) distanceTravelled);
	
		if (randomDistance < MEDIUM_THRESHOLD && distanceTravelled < HARD_THRESHOLD) {
			return ChooseEasy();
		} else if (randomDistance < HARD_THRESHOLD) {
			return ChooseMedium();
		} else {
			return ChooseHard();
		}
	}
	
	GameObject ChooseEasy() {
		return ChooseFrom (easy);
	}
	
	GameObject ChooseMedium() {
		return ChooseFrom (medium);
	}
	
	GameObject ChooseHard() {
		return ChooseFrom (hard);
	}
	
	GameObject ChooseFrom(GameObject[] levels) {
		return levels [random.Next (levels.Length)];
	}
}
