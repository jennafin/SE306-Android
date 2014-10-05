using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 1;
	public float weaponDamage = 1;
	public LayerMask whatToHit;

	float timeToFire = 0;
	Transform firePoint;
	Transform playerPosition;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");	
	}
	
	// Update is called once per frame
	void Update () {
		playerPosition = GameObject.FindWithTag("Player").transform;
		if (Time.time > timeToFire) {
				timeToFire = Time.time + (1 / fireRate);
				Shoot ();
		}
	}

	void Shoot () {
		Vector2 targetPosition = new Vector2 (playerPosition.position.x, playerPosition.position.y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, targetPosition - firePointPosition, 100, whatToHit);
		Debug.DrawLine (firePointPosition, targetPosition, Color.red);
		if (hit.collider != null) {
				Debug.Log ("Enemy hit " + hit.collider.name);
		}
	}
}
