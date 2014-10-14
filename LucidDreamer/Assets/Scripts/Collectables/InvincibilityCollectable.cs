using UnityEngine;
using System.Collections;

public class InvincibilityCollectable : Collectable {
	
	protected override int LifeSpan 
	{
		get { return 1000; }
	}
	
	protected override void InitiateCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().isInvincible = true;
	}
	
	protected override void RevokeCollectableBehaviour (GameControllerScript gameController) {
		gameController.getMainCharacter ().isInvincible = false;
	}
	
}