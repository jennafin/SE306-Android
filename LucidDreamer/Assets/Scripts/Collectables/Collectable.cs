using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour {

	// Length of time that this collectable lasts (in frames) 
	private int DEFAULT_LIFE_SPAN = 1;

	protected virtual int LifeSpan 
	{
		get { return DEFAULT_LIFE_SPAN; }
	}

	// Keeps track of how long this collectable has left to live.
	// A negative value signifies the collectable hasn't been used yet.
	private int framesOfLifeRemaining;

	public Collectable() 
	{
		this.framesOfLifeRemaining = this.LifeSpan;
	}

	/**
	 * Either applies or revokes the specific behaviour for this collectable.
	 * This is the public access point for users of this class. 
	 * 
	 * @return true if this collectable still has usable life
	 *         false if this collectable has been used up
	 */
	public bool UseOneFrame(GameControllerScript gameController) {
		if (framesOfLifeRemaining == LifeSpan)
		{
			InitiateCollectableBehaviour(gameController);
			framesOfLifeRemaining -= 1;
			return true;
		}
		else if (framesOfLifeRemaining > 0)
		{
			UpdateCollectableBehaviour(gameController, framesOfLifeRemaining);
			framesOfLifeRemaining -= 1;
			return true;
		}
		else
		{
			RevokeCollectableBehaviour(gameController);
			return false;
		}
	}

	/**
	 * Make any changes to the game controller that will introduce this collectable's behaviour
	 * into gameplay.
	 * 
	 * This must be implemented in subclasses.
	 */
	protected abstract void InitiateCollectableBehaviour (GameControllerScript gameController);

	/**
	 * Make any changes to the game controller, dependent on this collectables remaining life.
	 * 
	 * This does not have to be overriden by subclasses. By default it does nothing.
	 */
	protected virtual void UpdateCollectableBehaviour (GameControllerScript gameController, int framesOfLifeRemaining)
	{
	}

	/**
	 * Make any changes necessary to revoke this collectable from gameplay
	 * 
	 * This does not have to be overriden by subclasses. By default it does nothing.
	 */ 
	protected virtual void RevokeCollectableBehaviour (GameControllerScript gameController)
	{
	}
}
