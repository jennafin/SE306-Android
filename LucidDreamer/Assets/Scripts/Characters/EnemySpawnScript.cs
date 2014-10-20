using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour
{
		public GameObject mathsObject;
		public GameObject scienceObject;
		
		Theme currentTheme;
		
		public void SpawnWithTheme (Theme theme)
		{
			Debug.Log ("Spawning with theme: " + currentTheme);
			switch (currentTheme) {
				case Theme.Maths:
					Instantiate (mathsObject);
					break;
				case Theme.Science:
					Instantiate (scienceObject);
					break;
				default:
					Instantiate (mathsObject);
					break;
			}
	
		}
		
		void Instantiate(GameObject obj)
		{
			if (obj) {
				Instantiate (obj, transform.position, Quaternion.identity);
			}
		}
}