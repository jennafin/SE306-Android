using UnityEngine;
using System.Collections;

public class CollectableGeneratorScript : MonoBehaviour {
	
	public GameObject[] collectableTypes;
	public float spawnMin = 2f;
	public float spawnMax = 5f;
	
	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	void Spawn () {
		Instantiate(collectableTypes [Random.Range (0, collectableTypes.Length)], transform.position, Quaternion.identity);
		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}
}