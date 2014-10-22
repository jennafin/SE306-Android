using UnityEngine;
using System.Collections;

public class MultiplierCollectable : Collectable {

	protected override float LifeSpan 
	{
		get { return 10; }
	}
	
	protected override Color ParticleEmitterColor
	{
		get { return Color.blue; }
	}

	public int multiplier = 10;

	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) 
	{
		gameController.GetScoreTrackingSystem ().AddMultiplier (this.multiplier);
	}

	protected override void RevokeCollectableBehaviour (GameControllerScript gameController)
	{
		gameController.GetScoreTrackingSystem ().RemoveMultiplier (this.multiplier);
	}

}
