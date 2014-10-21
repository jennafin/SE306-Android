using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour
{

	public Transform player;
	public float distanceAhead = 22f;
	public float distanceBehind = 4f;
	public float yOffset = 3f;
	public float smooth = 0.1f;
	public float minHeight;
	private float xOffset;
	private float zOffset;
	private float currentLerpTime;
	private float startYPosition;
	private float endYPosition;
	
	void Start ()
	{
		UpdateOffsets ();
		startYPosition = player.position.y + yOffset;
	}

	void Update ()
	{
		// Update the position if being played in the editor, since it's useful to be able to tweak settings
		#if UNITY_EDITOR
		UpdateOffsets();
		#endif

		// Mathf.Lerp is used to add a delay to the change in camera y position
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > smooth) {
			currentLerpTime = 0f;
			startYPosition = endYPosition;
			endYPosition = player.position.y + yOffset;
		}
		float perc = currentLerpTime / smooth;

		// Update the camera transform to follow the player
		transform.position = new Vector3 (player.position.x + xOffset, Mathf.Lerp (startYPosition, endYPosition, perc), zOffset); 
	}

	void UpdateOffsets ()
	{
		// Calculate how much wider or higher the view gets for every unit distance away from the camera.
		// For example, if the FOV is 90 degrees and the camera is square, this value is exactly 2 for both
		float fovGradientY = 2f * Mathf.Tan (camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
		float fovGradientX = camera.aspect * fovGradientY;

		// Calculate the minimum distance that the camera needs to cover horizontally
		float minWidth = distanceAhead + distanceBehind;
		
		// Calculate how far back the camera needs to be in order to ensure the width is at least minWidth and
		// the height is at least minHeight
		zOffset = Mathf.Max (minWidth / fovGradientX, minHeight / fovGradientY);

		// Calculate the resulting camera width and height in the platform plane
		float cameraWidth = zOffset * fovGradientX;
		//float cameraHeight = zOffset * fovGradientY;

		// Calculate the X offset such that the look-ahead distance is always distanceBehind, but if the camera
		// covers more than the minimum width, the extra width is gained behind the character in order to give the
		// same look-ahead distance on all devices.
		xOffset = distanceAhead - 0.5f * cameraWidth;

		// Flip the sign of zOffset so that it looks towards positive Z at the platform plane
		zOffset = -zOffset;
	}
}