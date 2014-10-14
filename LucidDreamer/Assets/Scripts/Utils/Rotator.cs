using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	public float speed = 1.0f;								// speed multiplier for rotation
	public enum rotationAxis { X, Y, Z };					// enum to dictact axis of rotation
	public rotationAxis axisOfRotation = rotationAxis.Y;	// 
	private float x, y, z;									//

	// initialise rotation axis
	void Start () 
	{	
		switch (axisOfRotation) {
		case rotationAxis.X:
			x = 15f;
			break;
		case rotationAxis.Y:
			y = 15f;
			break;
		case rotationAxis.Z:
			z = 15f;
			break;
		}
	}

	// rotates GameObject around the axis of rotation at the speed specified
	void Update ()
	{
		transform.Rotate (new Vector3 (x, y, z) * Time.deltaTime * speed);
	}
}
