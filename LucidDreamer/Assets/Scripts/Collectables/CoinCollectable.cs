using UnityEngine;
using System.Collections;

public class CoinCollectable : Collectable {

	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.IncrementCoins (10);
	}

}
