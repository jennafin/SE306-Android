using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class TotalDistanceManager {
	
	private DistanceEntry totalDistance;
	
	
	public void Load()
	{
		//If not blank then load it
		if(File.Exists(Application.persistentDataPath + "/distance.dat"))
		{
			//Binary formatter for loading back
			BinaryFormatter bf = new BinaryFormatter();
			//Get the file
			FileStream f = File.Open(Application.persistentDataPath + "/distance.dat", FileMode.Open);
			//Load back the scores
			totalDistance = (DistanceEntry)bf.Deserialize(f);
			f.Close();
		}
	}
	
	public void UpdateDistance(int distance)
	{
		totalDistance.distance += distance;
	}
	
	public DistanceEntry GetTotalDistance()
	{
		return totalDistance;
	}
	
	public void SaveDistance()
	{
		//Get a binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//Create a file
		FileStream f = File.Open(Application.persistentDataPath + "/distance.dat", FileMode.OpenOrCreate);
		//Save the scores
		bf.Serialize(f, totalDistance);
		f.Close();
	}
	
}
