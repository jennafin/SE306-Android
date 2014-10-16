using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
	
		public void OnCollision (GameControllerScript gameController)
		{
				this.CollisionBehaviour (gameController);
				Destroy (gameObject);
		}
	
		public abstract void CollisionBehaviour (GameControllerScript gameController);
}
