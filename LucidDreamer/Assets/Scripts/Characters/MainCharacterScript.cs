using UnityEngine;
using System.Collections;


public class MainCharacterScript : MonoBehaviour {

	public GameControllerScript gameControllerScript;

	private float jumpForce = 750f;

	public float speed = 1f; // meters per second

	bool hasJumped = false;
	bool hasDoubleJumped = false;
	bool isGrounded = false;   // Whether the character is on the ground or not.

	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask ground;

	void Update() {
		updateIsGrounded ();
		bool userPressJump = Input.GetButtonDown ("Jump") || Input.GetButtonDown ("Fire1");
		if (userPressJump) {
			if (isGrounded) {
				Jump ();
			} else if (!isGrounded && !hasDoubleJumped) {
				DoubleJump ();
			}
		}
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}

	// Jumps
	void Jump() {
		addJumpForce (jumpForce);
	}

	// Performs a double jump
	void DoubleJump() {
		hasDoubleJumped = true;
		addJumpForce (jumpForce);
	}

	// Adds a upwards jump force
	void addJumpForce(float force) {
		Vector3 velocity = rigidbody2D.velocity;
		velocity.y = 0;
		rigidbody2D.velocity = velocity;
		rigidbody2D.AddForce(new Vector2(0, force));
	}

	// Checks whether alex is on the ground
	void updateIsGrounded () {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, ground);
		if (isGrounded) {
			hasDoubleJumped = false;
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		this.gameControllerScript.CharacterColliderWith (col);
	}

}
