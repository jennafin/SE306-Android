using UnityEngine;
using System.Collections;

public class PowerUpCollectable : Collectable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void CollectedBehaviour () {
		Debug.Log ("You got a power-up!", this);
	}

}
