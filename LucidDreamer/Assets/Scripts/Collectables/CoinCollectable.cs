using UnityEngine;
using System.Collections;

public class CoinCollectable : Collectable {

	protected override void ApplyCollectableBehaviour (GameControllerScript gameController) {
		gameController.IncrementCoins (10);
	}

}
