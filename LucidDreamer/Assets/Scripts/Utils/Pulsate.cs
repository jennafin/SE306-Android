using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {

	public float scaleFactor = 2.0f; 		// scale factor
	public float scaleSpeed = 1f;			// interpolation time
	
	private float currentLerpTime = 0; 		// to track linear interpolation time 
	private Vector3 startSize;				
	private Vector3 endSize;
	
	// set start size and end sizes based off scale factor
	void Start () {
		startSize = transform.localScale;
		endSize = new Vector3 (startSize.x * scaleFactor
							, startSize.y * scaleFactor
							, startSize.z * scaleFactor);
	}
	
	// linearly interpolate between two sizes
	void Update () {
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > scaleSpeed) {
			currentLerpTime = 0f;
			
			// swap start and end sizes
			Vector3 tempSize = startSize;
			startSize = endSize;
			endSize = tempSize;
		}
		float perc = currentLerpTime / scaleSpeed;
		transform.localScale = Vector3.Lerp (startSize, endSize, perc);		
	}
}
