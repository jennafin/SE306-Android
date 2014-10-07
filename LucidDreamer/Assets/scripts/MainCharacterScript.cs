using UnityEngine;
using System.Collections;

public class MainCharacterScript : MonoBehaviour
{

		private GameControllerScript gameControllerScript;
		public float jumpForce = 100f;
		public float speed = 1f; // meters per second

		bool grounded = false;   // Whether the character is on the ground or not.
		public Transform groundCheck;
		float groundRadius = 0.2f;
		public LayerMask ground;

		void FixedUpdate ()
		{
				grounded = IsGrounded ();
				if (grounded && Input.GetButton ("Jump")) {
						rigidbody2D.AddForce (new Vector2 (0, jumpForce));
				}
				rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		}

		bool IsGrounded ()
		{
				return Physics2D.OverlapCircle (groundCheck.position, groundRadius, ground);
		}

		void OnCollisionEnter (Collision col)
		{
				gameControllerScript.characterCollisionWith (col);
		}
}
