using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class HighScoreManager {

	private List<ScoreEntry> highScores = new List<ScoreEntry>();


	public HighScoreManager()
	{
		//If not blank then load it
		if(File.Exists(Application.persistentDataPath + "/highscores.dat"))
		{
			//Binary formatter for loading back
			var b = new BinaryFormatter();
			//Get the file
			var f = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Open);
			//Load back the scores
			highScores = (List<ScoreEntry>)b.Deserialize(f);
			f.Close();
		}

	}

	public void AddScore(string name, int score)
	{
		highScores.Add (new ScoreEntry (name, score));
	}

	public List<ScoreEntry> GetAllScores()
	{
		return highScores;
	}

	public List<ScoreEntry> GetTopTenScores()
	{
		List<ScoreEntry> SortedList = highScores.OrderBy(o=>o.score).ToList();
		int length = SortedList.Count;
		if (length > 10) {
						return SortedList.GetRange (0, 9);				
				} else if (length == 0) {
						return new List<ScoreEntry>();
				} else {
						return SortedList.GetRange(0,length-1);
				}
	}

	void SaveScores()
	{
		//Get a binary formatter
		var b = new BinaryFormatter();
		//Create a file
		var f = File.Create(Application.persistentDataPath + "/highscores.dat");
		//Save the scores
		b.Serialize(f, highScores);
		f.Close();
	}

}
