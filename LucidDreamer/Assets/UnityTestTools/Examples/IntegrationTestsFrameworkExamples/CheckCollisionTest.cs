using UnityEngine;
using System.Collections;

public class CheckCollisionTest : MonoBehaviour {

	GameControllerScript gameController;

	private float timePassed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		
		if (timePassed > 0.5F)
		{
			// We will have collided with the power-up by now.
			if (gameController.GetScoreTrackingSystem().currentScorePoints == 500) {
				IntegrationTest.Pass();
			}
		}
	}
}
