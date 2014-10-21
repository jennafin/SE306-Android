using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class PersistenceManager : MonoBehaviour {

	private PersistenceEntry persistence;
	
	
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
			persistence = (PersistenceEntry)bf.Deserialize(f);
			f.Close();
		}
		else 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream f = File.Create (Application.persistentDataPath + "/totalScore.dat");
			persistence = new PersistenceEntry();
			bf.Serialize(f, persistence);
			f.Close();
		}
	}
	
	public void UpdateScore(int score)
	{
		persistence.score += score;
	}
	
	public PersistenceEntry GetPersistenceEntry()
	{
		return persistence;
	}
	
	public int GetTotalScore() {
		return persistence.score;
	}
	
	public int GetTotalTime() {
		return persistence.timePlayed;
	}

	public void UpdateTime(int time)
	{
		persistence.timePlayed += time;
	}
	
	public void SavePersistence()
	{
		print ("Persistence time played!!: " + persistence.timePlayed);
		//Get a binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//Create a file
		FileStream f = File.Open(Application.persistentDataPath + "/totalScore.dat", FileMode.OpenOrCreate);
		//Save the scores
		bf.Serialize(f, persistence);
		f.Close();
	}
	
}
