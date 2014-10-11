using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreEntry {

	//Players name
	public string name;
	//Score
	public int score;
	
	public ScoreEntry(string n, int s)
	{
		this.name = n;
		this.score = s;
	}
}
