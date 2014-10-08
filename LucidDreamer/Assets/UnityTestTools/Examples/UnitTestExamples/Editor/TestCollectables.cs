using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace UnityTest
{
	[TestFixture]
	[Category("Collectables Tests")]
	internal class TestCollectables
	{	
		[Test]
		// Test that CoinCollectable correctly increments the GameController's coin count
		// when asked to perform its "collected behaviour"
		public void TestCoinCollectable()
		{
			GameControllerScript gameController = new GameControllerScript ();
			Collectable collectable = new CoinCollectable ();
			
			// Check that coin count initially starts at 0
			Assert.AreEqual (0, gameController.GetCoinsCollected());
			
			// Inform the collectable that it has been collected.
			collectable.CollectedBehaviour (gameController);
			
			// Check that the change has been stored in the game controller
			Assert.AreEqual (1, gameController.GetCoinsCollected());
		}
	}
}

