using UnityEngine;
using System.Collections;

public class CoinCollectable : Collectable {

	public override void CollectableBehaviour (GameControllerScript gameController) {
		gameController.IncrementCoins (10);
	}

}
