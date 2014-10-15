using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

		public PathedProjectile projectile;
		public float fireRate;
		public float speed = 1;

		float timeToFire = 0;

		// Set fire rate
		public void start ()
		{
				timeToFire = fireRate;
		}
	
		// If it's time top fire, instantiate and initialize a projectile
		void Update ()
		{

				if ((timeToFire -= Time.deltaTime) > 0)
						return;

				timeToFire = fireRate;
				var Projectile = (PathedProjectile)Instantiate (projectile, transform.position, transform.rotation);
				Projectile.Initialize (speed);
		}
}
