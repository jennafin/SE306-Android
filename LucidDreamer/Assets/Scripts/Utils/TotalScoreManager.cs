using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class TotalScoreManager : MonoBehaviour {

	private TotalScoreEntry totalScore;
	
	
	public void Load()
	{
		//If not blank then load it
		if(File.Exists(Application.persistentDataPath + "/totalScore.dat"))
		{
			//Binary formatter for loading back
			BinaryFormatter bf = new BinaryFormatter();
			//Get the file
			FileStream f = File.Open(Application.persistentDataPath + "/totalScore.dat", FileMode.Open);
			//Load back the scores
			totalScore = (TotalScoreEntry)bf.Deserialize(f);
			f.Close();
		}
		else 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream f = File.Create (Application.persistentDataPath + "/totalScore.dat");
			totalScore = new TotalScoreEntry(0);
			bf.Serialize(f, totalScore);
			f.Close();
		}
	}
	
	public void UpdateScore(int score)
	{
		totalScore.score += score;
	}
	
	public TotalScoreEntry GetTotalScore()
	{
		return totalScore;
	}
	
	public void SaveTotalScore()
	{
		//Get a binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//Create a file
		FileStream f = File.Open(Application.persistentDataPath + "/totalScore.dat", FileMode.OpenOrCreate);
		//Save the scores
		bf.Serialize(f, totalScore);
		f.Close();
	}
	
}
