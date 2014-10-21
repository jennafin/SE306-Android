using UnityEngine;
using System.Collections;

public class ChancePrefabSpawnScript : MonoBehaviour {

	public GameObject prefab;
	public int spawnChance;
	
	// Instantiate a prefab at this object's current location.
	void Start () {
		// This is necessary so that the parent of the spawned object is this object. 
		// This ensures that the gameobject is deleted when this PrefabSpawnScript is deleted.
		System.Random random = new System.Random ();
		if (random.Next (100) < spawnChance){
			GameObject go = Instantiate (prefab, transform.position, prefab.transform.rotation) as GameObject;
			go.transform.parent = this.transform;
		}
	}
}
