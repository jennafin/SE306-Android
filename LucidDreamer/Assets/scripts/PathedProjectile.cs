using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour {

	private Transform destination;
	private float speed;

	public void Initialize(float _speed){
		destination = GameObject.Find ("Target").transform;
		speed = _speed;
	}
	
	// Update is called once per frame
	public void Update () {
	
		transform.position = Vector3.MoveTowards (transform.position, destination.position, Time.deltaTime * speed);

		var distanceSquared = (destination.transform.position - transform.position).sqrMagnitude;
		if (distanceSquared > .01f * .01f) {
			return;
		}
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Alex") {
			Debug.Log ("Here");
			Destroy (gameObject);
		}
	}
}
