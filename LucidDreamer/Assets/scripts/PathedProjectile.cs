using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour {

	private Transform destination;
	private float speed;

	public void Initialize(Transform _destination, float _speed){
		destination = _destination;
		speed = _speed;
	}
	
	// Update is called once per frame
	public void Update () {
	
		transform.position = Vector3.MoveTowards (transform.position, destination.position, Time.deltaTime * speed);

		var distanceSquared = (destination.transform.position - transform.position).sqrMagnitude;
		if (distanceSquared > .01f * .01f)
			return;
		Destroy (gameObject);
	}

	/*void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Projectile Collision Detected on mover");
		if (coll.gameObject.tag == "Player")
			Debug.Log("Projectile Collision Detected on mover");
	}*/

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log("Projectile Collision Detected on mover");
			Destroy (gameObject);
		}
	}
}
