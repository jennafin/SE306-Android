using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class HighScoreManager {

	private List<ScoreEntry> highScores = new List<ScoreEntry>();
	

	public void Load()
	{
		//If not blank then load it
		if(File.Exists(Application.persistentDataPath + "/highscores.dat"))
		{
			//Binary formatter for loading back
			BinaryFormatter bf = new BinaryFormatter();
			//Get the file
			FileStream f = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Open);
			//Load back the scores
			highScores = (List<ScoreEntry>)bf.Deserialize(f);
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

	public ScoreEntry GetTopScore()
	{
		List<ScoreEntry> SortedList = Enumerable.Reverse (highScores.OrderBy(o=>o.score).ToList()).ToList();
		if (SortedList.Count < 1) 
		{
			return new ScoreEntry("none", 0);
		}
		return SortedList.First ();
	}

	public List<ScoreEntry> GetTopTenScores()
	{
		List<ScoreEntry> SortedList = Enumerable.Reverse (highScores.OrderBy(o=>o.score).ToList()).ToList();
		int length = SortedList.Count;
		if (length > 10) {
						return SortedList.GetRange (0, 10);				
				} else if (length == 0) {
						return new List<ScoreEntry>();
				} else {
						return SortedList.GetRange(0,length-1);
				}
	}

	public void SaveScores()
	{
		//Get a binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//Create a file
		FileStream f = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.OpenOrCreate);
		//Save the scores
		bf.Serialize(f, highScores);
		f.Close();
	}

}
