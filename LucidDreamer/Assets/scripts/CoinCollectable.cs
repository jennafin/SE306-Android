using UnityEngine;
using System.Collections;

public class CoinCollectable : Collectable {

	public override void CollectedBehaviour (GameControllerScript gameController) {
		gameController.IncrementCoins (1);
	}

}
