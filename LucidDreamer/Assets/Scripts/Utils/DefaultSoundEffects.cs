using UnityEngine;
using System.Collections;

public class DefaultSoundEffects : MonoBehaviour, SoundEffects {

	public AudioClip injured;
	public AudioClip death;
	public AudioClip jump;
	public AudioClip coinPickUp;
	
	public void playJumpSound() {
		play(jump);
	}
	
	public void playInjuredSound() {
		play (injured);
	}
	
	public void playDeathSound() {
		play (death);
	}
	
	public void playCoinPickUpSound() {
		play (coinPickUp);
	}
	
	void play(AudioClip sound) {
		if (! sound) {
			Debug.Log ("Sound is not initialized in inspector.");
		} else {
			AudioSource.PlayClipAtPoint(sound, new Vector3());
		}
	}
	
}
