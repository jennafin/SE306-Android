using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace UnityTest
{
	[TestFixture]
	[Category("Level Picker Tests")]
	internal class TestLevelPicker
	{	
		[Test]
		// Test that an easy level is returned when the distance travelled is in the easy region
		public void TestEasyLevel()
		{
			GameObject easyObject = new GameObject();
			GameObject[] easyObjects = new GameObject[] { easyObject };
			
			GameObject mediumObject = new GameObject();
			GameObject[] mediumObjects = new GameObject[] { mediumObject };
			
			GameObject hardObject = new GameObject();
			GameObject[] hardObjects = new GameObject[] { hardObject };
			
			LevelPicker levelPicker = new LevelPicker(easyObjects, mediumObjects, hardObjects);
			
			GameObject pickedLevel = levelPicker.ChooseLevel(500);
			Assert.AreSame(easyObject, pickedLevel);
		}
		
		[Test]
		// Test that either an easy or medium level is returned when the distance travelled is in the medium region
		public void TestMediumLevel()
		{
			GameObject easyObject = new GameObject();
			GameObject[] easyObjects = new GameObject[] { easyObject };
			
			GameObject mediumObject = new GameObject();
			GameObject[] mediumObjects = new GameObject[] { mediumObject };
			
			GameObject hardObject = new GameObject();
			GameObject[] hardObjects = new GameObject[] { hardObject };
			
			LevelPicker levelPicker = new LevelPicker(easyObjects, mediumObjects, hardObjects);
			
			GameObject pickedLevel = levelPicker.ChooseLevel(900);
			Assert.AreNotSame(hardObject, pickedLevel);
		}
	}
}

