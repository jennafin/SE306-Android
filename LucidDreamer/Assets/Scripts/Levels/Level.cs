using UnityEngine;
using System.Collections;

public class Level
{

		GameObject prefab;
		Bounds bounds;
		float maxX;
		float minX;
		float maxY;
		float minY;
		float width;
		float height;

		public Level (GameObject prefab, Theme theme, Vector3 position, Quaternion rotation)
		{
				this.prefab = MonoBehaviour.Instantiate (prefab, position, rotation) as GameObject;

				// Calculate width and height
				this.bounds = this.prefab.renderer.bounds;

				foreach (var r in this.prefab.GetComponentsInChildren<Renderer>()) {
						this.bounds.Encapsulate (r.bounds);
				}

				this.maxX = bounds.max.x;
				this.minX = bounds.min.x;
				this.maxY = bounds.max.y;
				this.minY = bounds.min.y;

				this.width = maxX - minX;
				this.height = maxY - minY;
		}

		public GameObject Prefab ()
		{
				return prefab;
		}

		public float Width ()
		{
				return width;
		}

		public float Height ()
		{
				return height;
		}

		public float MaxX ()
		{
				return maxX;
		}

		public float MinX ()
		{
				return minX;
		}

		public float MaxY ()
		{
				return maxY;
		}

		public float MinY ()
		{
				return minY;
		}
}
