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
		
		public void OnDeath()
		{
			Debug.Log("Oops, I died");
			rigidbody2D.velocity = new Vector3(20, 4, 0);
			rigidbody2D.angularVelocity = 100;
			Invoke("DestroyGameObject", 0.5F);
		}
		
		private void DestroyGameObject()
		{
			Destroy(gameObject);
		}
}
