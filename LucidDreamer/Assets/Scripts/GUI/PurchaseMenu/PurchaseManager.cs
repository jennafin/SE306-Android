using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;


public class PurchaseManager {

	private const string PURCHASES = "/purchases.dat";

	private Purchases purchases;

	public void Load()
	{
		//If not blank then load it
		if(File.Exists(Application.persistentDataPath + PURCHASES))
		{
			//Binary formatter for loading back
			BinaryFormatter bf = new BinaryFormatter();
			//Get the file
			FileStream f = File.Open(Application.persistentDataPath + PURCHASES, FileMode.Open);
			//Load back the scores
			purchases = (Purchases)bf.Deserialize(f);
			f.Close();
		}
	}

	public void SavePurchases()
	{
		//Get a binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//Create a file
		FileStream f = File.Open(Application.persistentDataPath + PURCHASES, FileMode.OpenOrCreate);
		//Save the scores
		bf.Serialize(f, purchases);
		f.Close();
	}

	public void Set4Lives(bool enabled) {
		purchases.Lives_4 = enabled;
	}

	public void Set5Lives(bool enabled) {
		purchases.Lives_5 = enabled;
	}

	public bool Get4Lives() {
		return purchases.Lives_4;
	}

	public bool Get5Lives() {
		return purchases.Lives_5;
	}
}
