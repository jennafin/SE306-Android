using UnityEngine;
using System.Collections;

public class LucidCollectable : Collectable {

	protected override void InitiateCollectableBehaviour(GameControllerScript gameController) {
		gameController.AddLucidPower(0.7f);
	}
}
