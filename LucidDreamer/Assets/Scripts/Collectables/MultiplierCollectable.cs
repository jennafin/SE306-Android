using UnityEngine;
using System.Collections;

public class MultiplierCollectable : Collectable {

	protected override int LifeSpan 
	{
		get { return 1000; }
	}

	public int multiplier = 10;

	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) 
	{
		gameController.getScoreTrackingSystem ().AddMultiplier (this.multiplier);
	}

	protected override void RevokeCollectableBehaviour (GameControllerScript gameController)
	{
		gameController.getScoreTrackingSystem ().RemoveMultiplier (this.multiplier);
	}

}
