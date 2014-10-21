using UnityEngine;
using System.Collections;

public class SuperJumpCollectable : Collectable {

	protected override int LifeSpan 
	{
		get { return 400; }
	}
	
	protected override Color ParticleEmitterColor
	{
		get { return Color.yellow; }
	}
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().startSuperJump ();
	}

	protected override void RevokeCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().endSuperJump ();
	}

}