using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour
{

		public Transform player;
		private float playerXOffset;

		void Start ()
		{
				playerXOffset = transform.position.x - player.position.x;
		}

		void Update ()
		{
				transform.position = new Vector3 (player.position.x + playerXOffset, 0, 0);
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.transform.parent) {
						Destroy (other.gameObject.transform.parent.gameObject);
				} else {
						Destroy (other.gameObject);
				}
		}
}
