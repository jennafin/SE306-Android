using UnityEngine;
using System.Collections;

public class ExtraLifeCollectable : Collectable {
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.IncrementLives (1);
	}
	
}
