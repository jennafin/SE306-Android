using UnityEngine;
using System.Collections;

public class MainCharacterScript : MonoBehaviour {

	public float jumpForce = 75f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
			bool jumpActive = (Input.GetButton ("Fire1") || Input.GetButton ("Jump"));
	
			if (jumpActive) {
					rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			}

	}
}
