using UnityEngine;
using System.Collections;


public class MainCharacterScript : MonoBehaviour {

	public GameControllerScript gameControllerScript;

	public float jumpForce = 100f;

	public float speed = 1f; // meters per second

	public GameObject GameController;


	bool grounded = false;   // Whether the character is on the ground or not.
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask ground;

	void FixedUpdate() {
		grounded = IsGrounded ();
		if (grounded && (Input.GetButton ("Jump") || Input.GetButton("Fire1"))) {
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}

	bool IsGrounded ()
	{
		return Physics2D.OverlapCircle (groundCheck.position, groundRadius, ground);
	}

	// Ugly hack used to get projectiles to reduce life points
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Dangerous") {
			GameControllerScript gameControllerScript = GameController.GetComponent<GameControllerScript>();
			gameControllerScript.characterColliderWith (col);
		}
	}
	

	void OnCollisionEnter2D(Collision2D col) {
				GameControllerScript gameControllerScript = GameController.GetComponent<GameControllerScript>();
				gameControllerScript.characterCollisionWith (col);
	}
}
