using UnityEngine;
using System.Collections;

public class PointsBoostCollectable : Collectable {

	public int points = 500;
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.GetScoreTrackingSystem ().AddPoints (this.points);
	}
	
}