using UnityEngine;
using System.Collections;

public class CoinCollectable : Collectable {

	public override void CollectedBehaviour () {
		Debug.Log ("You got a coin!", this);
	}

}
