using UnityEngine;
using System.Collections;

public class PowerUpCollectable : Collectable {

	public override void CollectedBehaviour () {
		Debug.Log ("You got a power-up!", this);
	}

}
