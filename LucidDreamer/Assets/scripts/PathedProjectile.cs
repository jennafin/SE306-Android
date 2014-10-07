using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour
{

		private Transform target;
		private float speed;

		// Set target and speed of projectile
		public void Initialize (float _speed, Transform _target)
		{
				target = _target;
				speed = _speed;
		}
	
		// Move projectile to its next position, If it's reached it's destination, destroy it.
		public void Update ()
		{
	
				transform.position = Vector3.MoveTowards (transform.position, target.position, Time.deltaTime * speed);

				var distanceSquared = (target.transform.position - transform.position).sqrMagnitude;
				if (distanceSquared > .01f * .01f) {
						return;
				}
				Destroy (gameObject);
		}

		// On collision, destroy projectile
		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.name == "Alex") {
						Destroy (gameObject);
				}
		}
}
