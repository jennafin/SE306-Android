using UnityEngine;
using System.Collections;

public class MainCharacterScript : MonoBehaviour {

	public float jumpForce = 10f;


	public float speed = 1f; // meters per second

	bool grounded = false;   // Whether the character is on the ground or not.
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask ground;
	
	// Update is called once per frame
	void Update() {
		if (grounded && Input.GetButton ("Jump")) {
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
	}

	void FixedUpdate(){
		grounded = IsGrounded ();
		rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
	}

	bool IsGrounded() {
		return Physics2D.OverlapCircle (groundCheck.position, groundRadius, ground);
	}
}
