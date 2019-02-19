using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class CannonController : MonoBehaviour {

    [Header("Rotation speed and angle of cannon:")]
    public float rotationSpeed;
	public float maxRotationAngle;

    // Public variable for testing launch force.
    [Header("Cannons force:")]
    public float launchForce;

    // The current character prefab.
    [Header("Current character prefab:")]
    public GameObject alienProjectile;
	public GameObject bearProjectile;
	public GameObject unicornProjectile;
	public GameObject batboyProjectile;
	public GameObject bigfootProjectile;

	public GameObject shadowPrefab;

    // The camera which follows the character.
    [Header("Camera object:")]
    public GameObject playerCamera;

	[Header("Breath power scale:")]
	public float breathPowerScale = 800;

	public float skimSpeed = 5.0f;

	public bool canPlay = false;

	GameObject projectile;

	GameObject buttonPrompt;
	Transform promptHeight;

	bool hasLaunched = false;
	public bool gameplayState = false;

	CannonScoreManager scoreManager;

	BreathRecogniser br = new BreathRecogniser();
	private float breathPressure;

	BreathMetre breathMetre;

	void Start () 
	{
		//Get reference to breath meter instance
		breathMetre = FindObjectOfType<BreathMetre>();
		scoreManager = FindObjectOfType<CannonScoreManager> ();

		br.BreathStarted += Br_BreathStarted;
		br.BreathComplete += Br_BreathComplete;

		buttonPrompt = GameObject.Find ("ButtonPrompt");
		promptHeight = GameObject.Find ("PromptHeightTransform").transform;
		buttonPrompt.SetActive (false);

		breathMetre.fillAmount = 0.0f;
	}

	public void Reset () 
	{
		breathMetre.reset = true;
		hasLaunched = false;
		launchForce = 0;

		GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");
		if (coins.Length > 0) 
		{
			for (int i = 0; i < coins.Length; i++) 
			{
				GameObject.Destroy (coins [i]);
			}
		}
		GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
		if (clouds.Length > 0) 
		{
			for (int i = 0; i < clouds.Length; i++) 
			{
				GameObject.Destroy (clouds [i]);
			}
		}
	}

	void Update() 
	{
		if (gameplayState) {

			if (!hasLaunched) {
				//Calculate launch force based on breath amount
				launchForce = breathMetre.fillAmount * breathPowerScale;

				// Launch on player action input.
				if ((Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ()) && canPlay && breathMetre.fillAmount > 0) 
				{
					hasLaunched = true;

					// Calculate launch vector.
					GameObject launchDir = GameObject.Find ("LaunchDirection");

					switch(CannonStaticValues.playerCharacter)
					{
					case CannonStaticValues.Characters.Alien:
						projectile = Instantiate (alienProjectile, transform.position, launchDir.transform.rotation);
						break;
					case CannonStaticValues.Characters.Batboy:
						projectile = Instantiate (batboyProjectile, transform.position, launchDir.transform.rotation);
						break;
					case CannonStaticValues.Characters.Bear:
						projectile = Instantiate (bearProjectile, transform.position, launchDir.transform.rotation);
						break;
					case CannonStaticValues.Characters.Unicorn:
						projectile = Instantiate (unicornProjectile, transform.position, launchDir.transform.rotation);
						break;
					case CannonStaticValues.Characters.BigFoot:
						projectile = Instantiate (bigfootProjectile, transform.position, launchDir.transform.rotation);
						break;
					default:
						projectile = Instantiate (alienProjectile, transform.position, launchDir.transform.rotation);
						break;
					}

					Vector3 shadowPosition = new Vector3 (projectile.transform.position.x, -1.0f, -9.0f);
					Instantiate (shadowPrefab, shadowPosition, Quaternion.identity);

					// Apply a force to the character projectile in the direction launch vector.
					Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D> ();
					projectileRb.AddForce (projectile.transform.up * launchForce);
					//projectileRb.AddTorque (20.0f);
				} 
				else
				{
					// If has not been launched oscillate between the launch angle.
					AxisRotation ();
				}
			} 
			else if (hasLaunched) 
			{
				if (projectile.transform.position.y < promptHeight.position.y) 
				{
					buttonPrompt.SetActive (true);

					if ((Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ()) && canPlay && breathMetre.fillAmount > 0) 
					{
						breathMetre.fillAmount -= 0.1f;
						scoreManager.scoreMultiplier += 1;

						Vector2 fireDirection = new Vector2 (0, 1);

						Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D> ();
						projectileRb.velocity = new Vector2 (projectileRb.velocity.x, skimSpeed);
					}
				} 
				else 
				{
					buttonPrompt.SetActive (false);
				}
				
			}
		}
	}

	void LateUpdate () 
	{
		if (!breathMetre.lockBar) 
		{
			canPlay = true;
		}
	}

	void AxisRotation ()
	{
		// Oscillate between the max and min angles and a set speed.
		float angle = Mathf.PingPong (Time.time * rotationSpeed, maxRotationAngle) - (maxRotationAngle / 2f);
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}

	private void Br_BreathStarted(object sender)
	{
		br.MaxBreathLength = FizzyoFramework.Instance.Device.maxBreathCalibrated;
		br.MaxPressure = FizzyoFramework.Instance.Device.maxPressureCalibrated;
	}

	private void Br_BreathComplete(object sender, ExhalationCompleteEventArgs e)
	{
		Debug.LogFormat("Breath Complete.\n Results: Quality [{0}] : Percentage [{1}] : Breath Full [{2}] : Breath Count [{3}] ", e.BreathQuality, e.BreathPercentage, e.IsBreathFull, e.BreathCount);
	}
}
