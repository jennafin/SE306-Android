using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		this.CollectedBehaviour ();
		Destroy (gameObject);
	}

	public abstract void CollectedBehaviour();
}
