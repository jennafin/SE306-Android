﻿using UnityEngine;
using System.Collections;

public class CoinCollectable : Collectable {

	public override void CollectedBehaviour () {
		//this.gameController.IncrementCoins (1);
		this.gameController.AddPoints (10);
	}

}
