using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour
{

		public GameObject[] obj;
		public float spawnMax = 5f;
		public float spawnMin = 4f;

		void Start ()
		{

				Spawn ();
	
		}
	
		void Spawn ()
		{

				Instantiate (obj [Random.Range (0, obj.Length)], transform.position, Quaternion.identity);
				Invoke ("Spawn", Random.Range (spawnMin, spawnMax));

		}
}