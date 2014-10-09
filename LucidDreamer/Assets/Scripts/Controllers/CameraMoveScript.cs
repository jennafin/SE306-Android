using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour
{
	
		public Transform player;
	
		// Update is called once per frame
		void Update ()
		{
				transform.position = new Vector3 (player.position.x + 5.5f, 3f, -7.5f);
		}
}