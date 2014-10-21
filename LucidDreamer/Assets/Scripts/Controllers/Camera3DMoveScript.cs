using UnityEngine;
using System.Collections;

public class Camera3DMoveScript : MonoBehaviour
{

	public Transform player;
	public float distanceBehind;
	public float distanceAboveTarget;
	public float distanceAboveMax;
	public float distanceAboveMin;
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
		float verticalTarget = player.position.y + distanceAboveTarget;
		float verticalDelta = transform.position.y - verticalTarget;
		verticalDelta *= Mathf.Pow (verticalFluidity, Time.deltaTime);

		// Constrain
		verticalDelta = Mathf.Min (verticalDelta, distanceAboveMax - distanceAboveTarget);
		verticalDelta = Mathf.Max (verticalDelta, distanceAboveMin - distanceAboveTarget);

		// Update the camera transform to follow the player
		transform.position = new Vector3 (player.position.x - distanceBehind, verticalTarget + verticalDelta, player.position.z);
	}
}