using UnityEngine;
using System.Collections;

public class MovingEnemyScript : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate (Vector2.one * moveSpeed);

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player")
			Debug.Log("Collision Detected on mover");
		
	}
}
