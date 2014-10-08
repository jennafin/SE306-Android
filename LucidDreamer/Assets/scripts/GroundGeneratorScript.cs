using UnityEngine;
using System.Collections;

public class GroundGeneratorScript : MonoBehaviour {
	
	public GameObject groundObject;
	public Transform player;
	private float playerXOffset;
	float spawnRate = 10.5f;
	
	// Use this for initialization
	void Start () {
		playerXOffset = transform.position.x - player.position.x;
		Update ();
		Spawn ();
	}
	
	void Update () {
		transform.position = new Vector3(player.position.x + playerXOffset, 2.409591f, -0.05384827f);
	}
	
	void Spawn () {
		Instantiate(groundObject, transform.position, Quaternion.identity);
		Invoke ("Spawn", spawnRate);
	}
}