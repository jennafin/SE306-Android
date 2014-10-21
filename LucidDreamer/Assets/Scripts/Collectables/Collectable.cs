using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Collectable : MonoBehaviour {

	// Length of time that this collectable lasts (in seconds) 
	private float DEFAULT_LIFE_SPAN = 0;

	/**
	 * This should be overridden to be the sound to be played when this collectable is collected.
	 *
	 * Defaults to null.
	 */
	public List<AudioClip> sound = new List<AudioClip>();

	protected virtual float LifeSpan 
	{
		get { return DEFAULT_LIFE_SPAN; }
	}
	
	protected virtual Color ParticleEmitterColor
	{
		get { return Color.clear; }
	}

	// Keeps track of how long this collectable has left to live.
	// A negative value signifies the collectable hasn't been used yet.
	protected float secondsOfLifeRemaining;

	public Collectable() 
	{
		this.secondsOfLifeRemaining = this.LifeSpan;
	}

	/**
	 * Either applies or revokes the specific behaviour for this collectable.
	 * This is the public access point for users of this class. 
	 * 
	 * @return true if this collectable still has usable life
	 *         false if this collectable has been used up
	 */
	public bool UseOneFrame(GameControllerScript gameController) {
		if (secondsOfLifeRemaining == LifeSpan)
		{
			// First time using the collectable
			InitiateCollectableBehaviour(gameController);
			StartParticleEmitter(gameController);
			secondsOfLifeRemaining -= Time.deltaTime;
			return true;
		}
		else if (secondsOfLifeRemaining > 0)
		{
			// Neither first nor last time using the collectable
			UpdateCollectableBehaviour(gameController);
			RestartParticleEmitter(gameController);
			secondsOfLifeRemaining -= Time.deltaTime;
			return true;
		}
		else
		{
			// Last time using the collectable
			RevokeCollectableBehaviour(gameController);
			StopParticleEmitter(gameController);
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
	 * Make any changes to the game controller that might depend on the collectables remaining life.
	 * 
	 * This does not have to be overriden by subclasses. By default it does nothing.
	 */
	protected virtual void UpdateCollectableBehaviour (GameControllerScript gameController)
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
	
	/**
	 * Start the main character's particle emitter, using this collectable's emitter color.
	 * Only start the emitter if this Collectable's emitter color is not set to Color.clear  
	 */
	private void StartParticleEmitter (GameControllerScript gameController)
	{
		if (this.ParticleEmitterColor != Color.clear)
		{
			gameController.getMainCharacter().StartParticleEmitter(this.ParticleEmitterColor);
		}
	}
	
	private void RestartParticleEmitter (GameControllerScript gameController)
	{
		MainCharacterScript mainCharacter = gameController.getMainCharacter();
		
		if (!mainCharacter.IsEmittingParticles())
		{
			StartParticleEmitter(gameController);
		}
	}
	
	/**
	 * Stop the main character's particle emitter.
	 */
	 private void StopParticleEmitter (GameControllerScript gameController)
	 {
		if (this.ParticleEmitterColor != Color.clear)
		{
	 		gameController.getMainCharacter().StopParticleEmitter();
	 	}
	 }
	


	/**
	 * Play this collectables sound.
	 */
	public void PlayCollectedSound ()
	{
		if (sound.Count >= 1) {
			System.Random random = new System.Random ();
			int number = random.Next(sound.Count);
			if (sound[number] != null){
				AudioSource.PlayClipAtPoint(sound[number], this.transform.position, 2.0f);
			}
		}
	}
}
