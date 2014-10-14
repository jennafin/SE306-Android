using UnityEngine;
using System.Collections;

public class PrefabSpawnScript : MonoBehaviour {

	public GameObject prefab;

	// Instantiate a prefab at this object's current location.
	void Start () {
		Instantiate (prefab, transform.position, prefab.transform.rotation);
	}

}
