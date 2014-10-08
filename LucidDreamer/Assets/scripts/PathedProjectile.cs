using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour
{
		private float initialXPosition;
		private Transform target;
		private float speed;
		private Vector3 targetPosition;

		// Set target and speed of projectile
		public void Initialize (float _speed, Transform _target)
		{
				initialXPosition = transform.position.x;
				target = _target;
				speed = _speed;
				targetPosition = new Vector3 ((transform.position.x - 10f), transform.position.y, 0f);
				
		}
	
		// Move projectile to its next position, If it's reached it's destination, destroy it.
		public void Update ()
		{
	
				transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);

				float distanceTravelled = initialXPosition - transform.position.x;
				if (distanceTravelled < 10f) {
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
