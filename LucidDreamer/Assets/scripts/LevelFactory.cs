using UnityEngine;
using System.Collections;

public class LevelFactory {

	Theme theme;
	GameObject prefab;

	public LevelFactory() {

	}

	public void setTheme(Theme theme) {
		this.theme = theme;
	}

	public void setLevelSegment(GameObject prefab) {
		this.prefab = prefab;
	}

	public GameObject build() {
		return null;
	}
}
