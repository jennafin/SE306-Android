using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainCharacterScript : MonoBehaviour {

	public GameControllerScript gameControllerScript;

	float jumpForce = 600f;
	float doubleJumpForce = 600f;
	float superJumpForce = 700f;

	float currentJumpForce;
	
	float speed = 10f;

	bool hasJumped = false;
	bool hasDoubleJumped = false;
	bool isGrounded = false;   // Whether the character is on the ground or not.

	public bool isInvincible = false; // Whether collisions should be ignored

	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask ground;

	public List<AudioClip> jumpSound = new List<AudioClip>();
	public AudioClip deathSound;
	public List<AudioClip> injuredSound = new List<AudioClip>();
	private bool soundEffectsOn;
	
	private bool isPaused = false;	// Whether the game is paused currently or not
	private Rect touchArea;

	private ParticleSystem particleSystem;

	
	void Start() {
		this.currentJumpForce = jumpForce;
		
		this.particleSystem = GetComponentInChildren<ParticleSystem>();
		StopParticleEmitter();
		RetrieveSettings ();
		touchArea = new Rect(0, 0, Screen.width, (Screen.height - Screen.height/5));
	}

	void Update() {
		updateIsGrounded ();
		bool userPressJump = (Input.GetButtonDown ("Jump") || JumpAreaTouched()) && !isPaused;
		if (userPressJump) {
			if (isGrounded) {
				Jump ();
			} else if (!isGrounded && !hasDoubleJumped) {
				DoubleJump ();
			}
		};
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}

	// Jumps
	void Jump() {
		PlayJumpSound ();
		addJumpForce (currentJumpForce);
	}

	// Performs a double jump
	void DoubleJump() {
		PlayJumpSound ();
		hasDoubleJumped = true;
		addJumpForce (doubleJumpForce);
	}

	// Adds a upwards jump force
	void addJumpForce(float force) {
		Vector3 velocity = rigidbody2D.velocity;
		velocity.y = 0;
		rigidbody2D.velocity = velocity;
		rigidbody2D.AddForce(new Vector2(0, force));
	}

	// Checks whether alex is on the ground
	void updateIsGrounded () {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, ground);
		if (isGrounded) {
			hasDoubleJumped = false;
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		this.gameControllerScript.CharacterColliderWith (col);
	}

	public void startSuperJump() {
		this.currentJumpForce = superJumpForce;
	}

	public void endSuperJump() {
		this.currentJumpForce = jumpForce;
	}
	
	public void PlayDeathSound() {
		PlaySound (deathSound);
	}
	
	public void PlayInjuredSound() {
		PlaySound (injuredSound[Random.Range(0,injuredSound.Count-1)]);
	}
	
	public void PlayJumpSound() {
		PlaySound (jumpSound[Random.Range(0,jumpSound.Count-1)]);
	}
	
	private void PlaySound(AudioClip sound) {
		if (! sound) {
			Debug.Log ("Sound is not initialized in inspector.");
		} else if (soundEffectsOn) {
			AudioSource.PlayClipAtPoint(sound, this.transform.position, 1.0f);
		}
	}

	// Retrieves persisted settings regarding the activation of sound effects
	private void RetrieveSettings() {
		if (PlayerPrefs.HasKey ("SoundEffectsOption")) {
			soundEffectsOn = PlayerPrefs.GetInt("SoundEffectsOption") != 0;
		} else {
			soundEffectsOn = true;
		}
	}	
	
	// set boolean to prevent the jump ability	
	public void PauseJumpAbility() {
		isPaused = true;
	}
	
	// set boolean to allow the jump ability
	public void UnpauseJumpAbility() {
		isPaused = false;
	}
	
	// checks if user input is within bounds of the area for triggering a jump
	private bool JumpAreaTouched() {
		if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began)) {
			if (touchArea.Contains(Input.GetTouch(0).position)) {
				return true;
			}
		}
		return false;
	}
	
	public void StartParticleEmitter(Color color)
	{
		this.particleSystem.startColor = color;
		this.particleSystem.enableEmission = true;
	}
	
	public void StopParticleEmitter()
	{
		this.particleSystem.enableEmission = false;
	}
	
	public bool IsEmittingParticles()
	{
		return this.particleSystem.enableEmission;
	}
}
