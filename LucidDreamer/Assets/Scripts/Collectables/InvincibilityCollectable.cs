using UnityEngine;
using System.Collections;

public class InvincibilityCollectable : Collectable {
	
	protected override float LifeSpan 
	{
		get { return 5; }
	}
	
	protected override Color ParticleEmitterColor
	{
		get { return Color.red; }
	}
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().isInvincible = true;
	}
	
	protected override void UpdateCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter().isInvincible = true;
	}
	
	protected override void RevokeCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().isInvincible = false;
	}
	
}