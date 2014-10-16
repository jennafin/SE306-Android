using UnityEngine;
using System.Collections;

public class PrefabSpawnScript : MonoBehaviour {

	public GameObject prefab;

	// Instantiate a prefab at this object's current location.
	void Start () {
		// This is necessary so that the parent of the spawned object is this object. 
		// This ensures that the gameobject is deleted when this PrefabSpawnScript is deleted.
		GameObject go = Instantiate (prefab, transform.position, prefab.transform.rotation) as GameObject;
		go.transform.parent = this.transform;
	}

}
