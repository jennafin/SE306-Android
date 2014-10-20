using UnityEngine;
using System.Collections;

public class Camera3DMoveScript : MonoBehaviour
{

	public Transform player;
	public float distanceBehind;
	public float distanceAbove;
	public float verticalFluidity;
	
	void Start ()
	{
		UpdateOffsets ();
	}

	void Update ()
	{
		UpdateOffsets();

	}

	void UpdateOffsets ()
	{
		float verticalTarget = player.position.y + distanceAbove;
		float verticalDelta = verticalTarget - transform.position.y;
		float verticalPosition = verticalTarget - verticalDelta * Mathf.Pow (verticalFluidity, Time.deltaTime);

		// Update the camera transform to follow the player
		transform.position = new Vector3 (player.position.x - distanceBehind, verticalPosition, player.position.z);
	}
}