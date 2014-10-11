using UnityEngine;
using System.Collections;

public class PowerUpCollectable : Collectable {

	public override void CollectableBehaviour (GameControllerScript gameController) {
		Debug.Log ("PowerUpCollectable: You got a power-up!", this);
	}

}
