using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	//Transform destination;
	public PathedProjectile projectile;

	public float fireRate;
	public float speed = 1;
	//public float weaponDamage = 1;

	float timeToFire = 0;
	//Transform firePoint;

	public void start() {
		timeToFire = fireRate;
	}
	
	// Update is called once per frame
	void Update () {

		if ((timeToFire -= Time.deltaTime) > 0)
			return;

		timeToFire = fireRate;
		var Projectile = (PathedProjectile)Instantiate(projectile, transform.position, transform.rotation);
		Projectile.Initialize(speed);
	}
}
