using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMax = 1f;
	public float spawnMin = 2f;

	void Start () {

		Spawn();
	
	}
	
	void Spawn() {

		Instantiate (obj [Random.Range (0, obj.Length)], transform.position, Quaternion.identity);
		Invoke("Spawn", Random.Range (spawnMin, spawnMax));

	}
}