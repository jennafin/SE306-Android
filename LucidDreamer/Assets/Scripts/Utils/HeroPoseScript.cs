﻿using UnityEngine;
using System.Collections;

public class HeroPoseScript : MonoBehaviour
{

		// Update is called once per frame
		void Update ()
		{
				// exit game on escape/back button
				if (Input.GetKeyDown (KeyCode.Escape)) {
						
						Application.LoadLevel ("MainMenu");
				}
		}
}