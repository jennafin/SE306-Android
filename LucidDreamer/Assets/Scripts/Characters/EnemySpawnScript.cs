using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour
{
		public GameObject mathsObject;
		public GameObject scienceObject;
				
		public GameObject SpawnWithTheme (Theme theme)
		{
			GameObject obj;
			switch (theme) {
				case Theme.Maths:
					obj = Instantiate (mathsObject);
					break;
				case Theme.Science:
					obj = Instantiate (scienceObject);
					break;
				default:
					obj = Instantiate (mathsObject);
					break;
			}
			return obj;
		}
		
		GameObject Instantiate(GameObject obj)
		{
			GameObject newObject = null;
			if (obj) {
				newObject = (GameObject) Instantiate (obj, transform.position, Quaternion.identity);
			}
			return newObject;
		}
}