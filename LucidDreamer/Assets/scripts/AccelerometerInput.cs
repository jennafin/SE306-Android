using UnityEngine;
using System.Collections;
using System;

public class AccelerometerInput : MonoBehaviour 
{
	private double d1 = 0;
	private const double delta = 0.1;

	void Update () 
	{
		double d2 = Math.Sqrt (Math.Pow (Input.acceleration.x, 2) + 
		                       Math.Pow (Input.acceleration.y, 2) + 
		                       Math.Pow (Input.acceleration.z, 2));

		double velocity = d2 - d1;
		Debug.Log ("The velocity is " + velocity);
		d1 = d2;
		if (velocity > delta) {
			// Oh no it's moving to much
		}
	}
}