using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	public float offset = 1f;			// translation offset
	public float floatSmooth = 1f;		// translation time
	
	private Vector3 startPosition;		// initial position of GameObject
	private Vector3 topPosition;		// Top postion after adding offset
	private Vector3 bottomPosition;		// Bottom position
	private float currentLerpTime;		// track linear interpolation
	
	// set top and bottom positions based off starting with offset applied
	void Start () {
		startPosition = transform.position;
		topPosition = startPosition;
		topPosition.y += offset;
		bottomPosition = startPosition;
		bottomPosition.y -= offset;
	}
	
	// linearly interpolate position between top and bottom limits
	void Update () {
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > floatSmooth) {
			currentLerpTime = 0f;
			
			// swap top and bottom positions
			Vector3 tempPosition = topPosition;
			topPosition = bottomPosition;
			bottomPosition = tempPosition;
		}
		float perc = currentLerpTime / floatSmooth;
		transform.position = Vector3.Lerp (topPosition, bottomPosition, perc);	
	}
}
