using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Transform destination;
	public PathedProjectile projectile;

	public float fireRate;
	public float speed = 1;
	//public float weaponDamage = 1;

	float timeToFire = 0;
	//Transform firePoint;

	/* Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");	
	}*/

	public void start() {
		timeToFire = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		//destination = GameObject.transform;

		if ((timeToFire -= Time.deltaTime) > 0)
			return;

		timeToFire = fireRate;
		var Projectile = (PathedProjectile)Instantiate(projectile, transform.position, transform.rotation);
		Projectile.Initialize(destination, speed);
		/*if (Time.time > timeToFire) {
				timeToFire = Time.time + (1 / fireRate);
				Shoot ();
		}*/
	}

	public void OnDrawGizmos() {
		if (destination == null)
			return;
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, destination.position);
	}

	/*void Shoot () {
		Vector2 targetPosition = new Vector2 (playerPosition.position.x, playerPosition.position.y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, targetPosition - firePointPosition, 100, whatToHit);
		Debug.DrawLine (firePointPosition, targetPosition, Color.red);
		if (hit.collider != null) {
				Debug.Log ("Enemy hit " + hit.collider.name);
		}
	}*/
}
