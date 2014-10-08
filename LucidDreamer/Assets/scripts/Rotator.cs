using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{

		public float speed = 1.0f;

		// Update is called once per frame
		void Update ()
		{
				transform.Rotate (new Vector3 (0, 15, 0) * Time.deltaTime * speed);
		}
}
