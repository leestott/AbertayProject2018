using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class CannonController : MonoBehaviour {

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

	void Start () 
	{
		foreach (Transform child in transform) 
		{
			if (child.name == "CannonBody") 
			{
				cannonBody = child.gameObject;
			}
		}

		if (cannonBody != null)
		{
			anim = cannonBody.GetComponent<Animator> ();
		}

		fillAmount = 0.0f;
		breathMetre.reset = true;
	}

	void Update () 
	{
		if (StaticGameState.currentGameState == StaticGameState.GameState.Gameplay) 
		{
			fillAmount = breathMetre.fillAmount;

			if (breathMetre.fillAmount >= 1.0f) 
			{
				breathMetre.fillAmount = 1.0f;
			}

			if (breathMetre.lockBar)
			{
				breathMetre.UpdateCannonBar ();
			}

			if (!hasSpawnedProjectile && anim.GetCurrentAnimatorStateInfo (0).IsName ("CannonFire")) 
			{
				hasSpawnedProjectile = true;
				launchForce = cannonBasePower;

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
				Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D> ();
				projectileRB.AddForce (launchDirection.transform.up * launchForce * fillAmount);

				boostParticle = projectile.GetComponentInChildren<ParticleSystem> ();

				hasSpawnedShadow = true;
				Vector3 shadowPosition = new Vector3 (projectile.transform.position.x, -3.0f, -9.0f);
				Instantiate (shadowPrefab, shadowPosition, Quaternion.identity);
			}

			if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && !hasLaunched && fillAmount > 0.0f) {
				hasLaunched = true;	
				anim.SetTrigger ("FireCannon");
			}
			if (hasLaunched && projectile != null) {
				Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D> ();
				//Debug.Log ("Projectile Velocity: " + projectileRB.velocity.x);

				if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && fillAmount >= decrementAmount) {
					Vector2 boostDirection = new Vector2 (1, 2);
					boostParticle.Play ();


					breathMetre.fillAmount -= decrementAmount;
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

			if (!hasLaunched) {
				AxisRotation ();
			}
		}
	}

	public void Reset () 
	{
		GameObject[] obstacleList = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i = 0; i < obstacleList.Length; i++) 
		{
			GameObject.Destroy (obstacleList [i]);
		}

		hasLaunched = false;
		gameOver = false;
		hasSpawnedProjectile = false;
		hasSpawnedShadow = false;
		fillAmount = 0.0f;
		breathMetre.reset = true;
	}

	void AxisRotation ()
	{
		// Oscillate between the max and min angles and a set speed.
		float angle = Mathf.PingPong (Time.time * rotationSpeed, maxRotationAngle) - (maxRotationAngle / 2f);
		cannonBody.transform.eulerAngles = new Vector3 (0, 0, angle);
	}


}
