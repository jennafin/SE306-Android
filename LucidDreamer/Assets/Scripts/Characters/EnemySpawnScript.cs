using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour
{
		public GameObject mathsObject;
		public GameObject scienceObject;
				
		public void SpawnWithTheme (Theme theme)
		{
			switch (theme) {
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