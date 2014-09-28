using UnityEngine;
using System.Collections;

public class GroundGeneratorScript : MonoBehaviour {
	
	public GameObject groundObject;
	float spawnRate = 0.5f;
	
	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	void Spawn () {
		Instantiate(groundObject, transform.position, Quaternion.identity);
		Invoke ("Spawn", spawnRate);
	}
}