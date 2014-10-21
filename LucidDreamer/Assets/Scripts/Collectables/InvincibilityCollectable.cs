using UnityEngine;
using System.Collections;

public class InvincibilityCollectable : Collectable {
	
	protected override int LifeSpan 
	{
		get { return 500; }
	}
	
	protected override Color ParticleEmitterColor
	{
		get { return Color.red; }
	}
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		MainCharacterScript mainCharacter = gameController.getMainCharacter ();
		mainCharacter.isInvincible = true;
	}
	
	protected override void RevokeCollectableBehaviour (GameControllerScript gameController) {
		MainCharacterScript mainCharacter = gameController.getMainCharacter ();
		mainCharacter.isInvincible = false;
	}
	
}