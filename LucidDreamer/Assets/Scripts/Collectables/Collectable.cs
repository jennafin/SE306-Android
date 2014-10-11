using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour {

	public abstract void CollectableBehaviour(GameControllerScript gameController);

}
