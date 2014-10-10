using UnityEngine;
using System.Collections;

public class MovingEnemyScript : MonoBehaviour
{

		public float walkSpeed = 5.0f;
		public float wallLeft = 0.0f;
		public float wallRight = 2.0f;
		float walkingDirection = 1.0f;
		Vector3 walkAmount;

		void Start ()
		{
	
		}
	
		// Move enemy to the left
		void Update ()
		{

				walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
		
				if (walkingDirection > 0.0f && transform.position.x >= wallRight)
						walkingDirection = -1.0f;
				else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
						walkingDirection = 1.0f;
		
				transform.Translate (walkAmount);
		}

		// Detect collisions
		void OnCollisionEnter2D (Collision2D coll)
		{
				if (coll.gameObject.tag == "Player")
						Debug.Log ("Collision Detected on mover");
		}
}
