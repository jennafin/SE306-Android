using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour {
	
	public void OnCollection(GameControllerScript gameController) {
		this.CollectedBehaviour (gameController);
		Destroy (gameObject);
	}

	public abstract void CollectedBehaviour(GameControllerScript gameController);
}
