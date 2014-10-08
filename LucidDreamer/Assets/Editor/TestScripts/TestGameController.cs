using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace UnityTest
{
	[TestFixture]
	[Category("Game Controller Tests")]
	internal class TestGameController
	{	
		[Test]
		// Test that the game controller's coin count can be incremented by a positive amount
		public void TestIncrementCoins()
		{
			GameControllerScript gameController = Substitute.For<GameControllerScript>();
			ScoreTrackingSystem scoreTracker = Substitute.For<ScoreTrackingSystem> ();
			gameController.setScoreTrackingSystem (scoreTracker);
			
			// Check that coin count initially starts at 0
			Assert.AreEqual (0, gameController.GetCoinsCollected());
			
			// Increment by a positive amount
			gameController.IncrementCoins (5);
			
			// Check that the change has been stored in the game controller
			Assert.AreEqual (5, gameController.GetCoinsCollected());
		}
		
		[Test]
		// Test that the game controller's coin count can be "incremented" by a negative amount
		public void TestDecrementCoins()
		{
			GameControllerScript gameController = Substitute.For<GameControllerScript>();
			ScoreTrackingSystem scoreTracker = Substitute.For<ScoreTrackingSystem> ();
			gameController.setScoreTrackingSystem (scoreTracker);

			// Check that coin count initially starts at 0
			Assert.AreEqual (0, gameController.GetCoinsCollected());
			
			// Increment by positive and negative amounts
			gameController.IncrementCoins (5);
			gameController.IncrementCoins (-2);
			
			// Check that the change has been stored in the game controller
			Assert.AreEqual (3, gameController.GetCoinsCollected());
		}
	}
}