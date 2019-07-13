using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class CannonController : MonoBehaviour {

	//Public and global variables
	[Header("Rotation speed and angle of cannon:")]
	public float rotationSpeed;
	public float maxRotationAngle;

	public float cannonBasePower;
	public float chargeAmount;

	public float minimumSpeed;
	public float speedDamping;

	public float boostSpeed;

	public float fillAmount = 0.0f;

	public bool gameOver = false;

	public GameObject alienPrefab;
	public GameObject batBoyPrefab;
	public GameObject bearPrefab;
	public GameObject bigfootPrefab;
	public GameObject unicornPrefab;

	bool hasLaunched = false;
	bool hasSpawnedProjectile = false;

	public GameObject launchDirection;
	GameObject cannonBody;

	GameObject projectile;

	public GameObject shadowPrefab;

	bool hasSpawnedShadow = false;

	ParticleSystem boostParticle;

	float launchForce;

	private Animator anim;

	public BreathMetre breathMetre;

	public float decrementAmount = 0.1f;

	public float distance;

	public CharacterAudioManager characterAudioManager;

	MinigameScoring minigameScoring;

	public AudioClip cannonRotateClip;

	AudioSource cannonAudioSource;

	void Start () 
	{
		//Get child cannon sprite
		foreach (Transform child in transform) 
		{
			if (child.name == "CannonBody") 
			{
				cannonBody = child.gameObject;
			}
		}

		//Initialise variables and get references
		if (cannonBody != null)
		{
			anim = cannonBody.GetComponent<Animator> ();
		}

		fillAmount = 0.0f;
		breathMetre.reset = true;

		characterAudioManager = GameObject.FindObjectOfType<CharacterAudioManager> ();
		minigameScoring = GameObject.FindObjectOfType<MinigameScoring> ();
		cannonAudioSource = GameObject.Find ("CannonAudioSource").GetComponent<AudioSource> ();

		cannonAudioSource.clip = cannonRotateClip;
		cannonAudioSource.Play ();
		cannonAudioSource.mute = true;
	}

	void Update () 
	{
		//If in gameplay state
		if (StaticGameState.currentGameState == StaticGameState.GameState.Gameplay) 
		{
			//Update fill amount using breath metre value
			fillAmount = breathMetre.fillAmount;

			//Cap fill amount to 1.0f to prevent launching exploit
			if (breathMetre.fillAmount >= 1.0f) 
			{
				breathMetre.fillAmount = 1.0f;
			}

			//Call custom function in breath bar to prevent bar lock animation
			if (breathMetre.lockBar)
			{
				breathMetre.UpdateCannonBar ();
			}

			//If cannon has finish charging and is currently firing
			if (!hasSpawnedProjectile && anim.GetCurrentAnimatorStateInfo (0).IsName ("CannonFire")) 
			{
				characterAudioManager.PlayerLaunch ();
				hasSpawnedProjectile = true;
				launchForce = cannonBasePower;

				//Instantiate player projectile with current selected character
				switch(StaticGameState.currentCharacter) 
				{
				case StaticGameState.CharacterState.Alien:
					projectile = Instantiate (alienPrefab, launchDirection.transform.position, launchDirection.transform.rotation) as GameObject;
					break;
				case StaticGameState.CharacterState.Batboy:
					projectile = Instantiate (batBoyPrefab, launchDirection.transform.position, launchDirection.transform.rotation) as GameObject;
					break;
				case StaticGameState.CharacterState.Bear:
					projectile = Instantiate (bearPrefab, launchDirection.transform.position, launchDirection.transform.rotation) as GameObject;
					break;
				case StaticGameState.CharacterState.Bigfoot:
					projectile = Instantiate (bigfootPrefab, launchDirection.transform.position, launchDirection.transform.rotation) as GameObject;
					break;
				case StaticGameState.CharacterState.Unicorn:
					projectile = Instantiate (unicornPrefab, launchDirection.transform.position, Quaternion.identity) as GameObject;
					break;
				default:
					projectile = Instantiate (alienPrefab, launchDirection.transform.position, launchDirection.transform.rotation) as GameObject;
					break;
				}
				//Get reference to projectile rigidbody2d
				Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D> ();
				//Appl launch force in cannon fire direction
				projectileRB.AddForce (launchDirection.transform.up * launchForce * fillAmount);

				//Get reference to boost particle system
				boostParticle = projectile.GetComponentInChildren<ParticleSystem> ();

				hasSpawnedShadow = true;
				Vector3 shadowPosition = new Vector3 (projectile.transform.position.x, -3.0f, -9.0f);
				Instantiate (shadowPrefab, shadowPosition, Quaternion.identity);
			}

			//If button input, start cannon fire animation
			if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && !hasLaunched && fillAmount > 0.0f) {
				hasLaunched = true;	
				anim.SetTrigger ("FireCannon");
				cannonAudioSource.mute = true;
			}
				
			//If projectile is currently in the air
			if (hasLaunched && projectile != null) {
				Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D> ();
				//Debug.Log ("Projectile Velocity: " + projectileRB.velocity.x);

				distance = projectile.transform.position.x;

				//If button input and breath bar filled over a minimum amount
				if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && fillAmount >= decrementAmount) {
					Vector2 boostDirection = new Vector2 (1, 2);
					boostParticle.Play ();
					//Decrement breath bar by decrement amount to limit amount of boost uses
					breathMetre.fillAmount -= decrementAmount;
					//Apply boost to player projectile
					if (projectileRB.velocity.y <= boostSpeed) {
						projectileRB.velocity = new Vector2 (projectileRB.velocity.x + boostSpeed, boostSpeed * 1.5f);
					} else {
						projectileRB.velocity = new Vector2 (projectileRB.velocity.x + boostSpeed, projectileRB.velocity.y + boostSpeed * 1.5f);
					}
				}

				if (projectileRB.velocity.x < minimumSpeed && projectile != null) {
					gameOver = true;	
				} else {
					gameOver = false;
				}
			}

			//If not launched yet rotate cannon
			if (!hasLaunched) {
				AxisRotation ();
				cannonAudioSource.mute = false;
			}
		}
	}

	public void Reset () 
	{
		//Destroy all obstacles in scene
		GameObject[] obstacleList = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i = 0; i < obstacleList.Length; i++) 
		{
			GameObject.Destroy (obstacleList [i]);
		}

		//Reset variables
		hasLaunched = false;
		gameOver = false;
		hasSpawnedProjectile = false;
		hasSpawnedShadow = false;
		fillAmount = 0.0f;
		breathMetre.reset = true;

		minigameScoring.AddScore (Mathf.RoundToInt (distance));
		distance = 0.0f;
	}

	void AxisRotation ()
	{
		// Oscillate between the max and min angles and a set speed.
		float angle = Mathf.PingPong (Time.time * rotationSpeed, maxRotationAngle) - (maxRotationAngle / 2f);
		cannonBody.transform.eulerAngles = new Vector3 (0, 0, angle);
	}


}
