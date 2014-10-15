using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	public float offset = 1f;				// translation offset
	public float smooth = 1f;				// translation time
	public enum Axis { X, Y, Z };
	public Axis directionOfMovement;		// float direction
	
	private Vector3 startPosition;			// initial position of GameObject
	private Vector3 positionPlusOffset;		// Top postion after adding offset
	private Vector3 positionMinusOffset;	// Bottom position
	private float currentLerpTime;			// track linear interpolation
	
	// set top and bottom positions based off starting with offset applied
	void Start () {
		startPosition = transform.position;
		positionPlusOffset = startPosition;
		positionMinusOffset = startPosition;
		
		switch(directionOfMovement) {
		case Axis.X:
			positionPlusOffset.x += offset;
			positionMinusOffset.x -= offset;
			break;
		case Axis.Y:
			positionPlusOffset.y += offset;
			positionMinusOffset.y -= offset;
			break;
		case Axis.Z:
			positionPlusOffset.z += offset;
			positionMinusOffset.z -= offset;
			break; 
		}
	}
	
	// linearly interpolate position between top and bottom limits
	void Update () {
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > smooth) {
			currentLerpTime = 0f;
			
			// swap top and bottom positions
			Vector3 tempPosition = positionPlusOffset;
			positionPlusOffset = positionMinusOffset;
			positionMinusOffset = tempPosition;
		}
		float perc = currentLerpTime / smooth;
		transform.position = Vector3.Lerp (positionPlusOffset, positionMinusOffset, perc);	
	}
}
