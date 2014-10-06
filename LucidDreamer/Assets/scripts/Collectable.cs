using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour {

	public GameControllerScript gameController;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			this.CollectedBehaviour ();
			Destroy (gameObject);
		}
	}

	public abstract void CollectedBehaviour();
}
