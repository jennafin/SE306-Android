using UnityEngine;
using System.Collections;

public class SuperJumpCollectable : Collectable {

	protected override float LifeSpan 
	{
		get { return 5; }
	}
	
	protected override Color ParticleEmitterColor
	{
		get { return Color.yellow; }
	}
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().startSuperJump ();
	}
	
	protected override void UpdateCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter().startSuperJump ();
	}

	protected override void RevokeCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().endSuperJump ();
	}

}