using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class AchievementsTrackerManager : MonoBehaviour {

	public AchievementsTracker achievementsTracker;
	
	
	public void Load()
	{
		//If not blank then load it
		if(File.Exists(Application.persistentDataPath + "/achievementsTracker.dat"))
		{
			//Binary formatter for loading back
			BinaryFormatter bf = new BinaryFormatter();
			//Get the file
			FileStream f = File.Open(Application.persistentDataPath + "/achievementsTracker.dat", FileMode.Open);
			//Load back the scores
			achievementsTracker = (AchievementsTracker)bf.Deserialize(f);
			f.Close();
		}
		else 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream f = File.Create (Application.persistentDataPath + "/achievementsTracker.dat");
			achievementsTracker = new AchievementsTracker(0);
			bf.Serialize(f, achievementsTracker);
			f.Close();
		}
	}
	
	public void SaveAchievements()
	{
		//Get a binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//Create a file
		FileStream f = File.Open(Application.persistentDataPath + "/achievementsTracker.dat", FileMode.OpenOrCreate);
		//Save the scores
		bf.Serialize(f, achievementsTracker);
		f.Close();
	}
	
}