using UnityEngine;
using System.Collections;

public class LevelFactory
{

		Theme theme;
		GameObject levelPrefab;
		Vector3 position;
		Quaternion rotation;

		public LevelFactory ()
		{
		}

		public void setTheme (Theme theme)
		{
				this.theme = theme;
		}

		public void setLevelSegment (GameObject levelPrefab)
		{
				this.levelPrefab = levelPrefab;
		}

		public void setPosition (Vector3 position)
		{
				this.position = position;
		}

		public void setRotation (Quaternion rotation)
		{
				this.rotation = rotation;
		}

		public Level build ()
		{
				return new Level (levelPrefab, theme, position, rotation);
		}
}
