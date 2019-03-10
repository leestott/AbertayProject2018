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
	public GameObject splashPrefab;

    // The camera which follows the character.
    [Header("Camera object:")]
    public GameObject playerCamera;

	[Header("Breath power scale:")]
	public float breathPowerScale = 800;

	bool hasSplashed = false;

	public float skimSpeed = 5.0f;

	public bool canPlay = false;

	GameObject projectile;

	GameObject buttonPrompt;
	Transform promptHeight;

	bool hasLaunched = false;
	bool hasSpawnedShadow = false;
	public bool gameplayState = false;

	CannonScoreManager scoreManager;

	BreathRecogniser br = new BreathRecogniser();
	private float breathPressure;

	BreathMetre breathMetre;

	public AudioClip cannonFireClip;
	AudioSource cannonAudioSource;

	AudioSource sfxSource;

	GameObject currentSplash;
	GameObject cannonSmoke;

	Animation splashAnim;

	CharacterAudioManager characterAudio;

	public AudioClip[] waterSkiffs;

    private GlobalSessionScore globalSessionScore;

	private SetDisplayInfo setDisplayInfo;

    void Start () 
	{
		//Get reference to breath meter instance
		breathMetre = FindObjectOfType<BreathMetre>();
		scoreManager = FindObjectOfType<CannonScoreManager> ();
        globalSessionScore = FindObjectOfType<GlobalSessionScore>();

        br.BreathStarted += Br_BreathStarted;
		br.BreathComplete += Br_BreathComplete;

		buttonPrompt = GameObject.Find ("ButtonPrompt");
		promptHeight = GameObject.Find ("PromptHeightTransform").transform;
		buttonPrompt.SetActive (false);

		breathMetre.fillAmount = 0.0f;

		cannonAudioSource = GetComponent<AudioSource> ();
		sfxSource = GameObject.Find ("SFXAudioSource").GetComponent<AudioSource> ();

		cannonSmoke = GameObject.Find ("CannonSmoke");

		characterAudio = GameObject.FindObjectOfType<CharacterAudioManager> ();

		cannonSmoke.SetActive (false);

		setDisplayInfo = GameObject.FindObjectOfType<SetDisplayInfo> ();
	}

    public void Reset()
    {
        breathMetre.reset = true;
		breathMetre.fillAmount = 0.0f;
        hasLaunched = false;
        hasSpawnedShadow = false;
        launchForce = 0;
        cannonSmoke.SetActive(false);

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        if (coins.Length > 0)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                GameObject.Destroy(coins[i]);
            }
        }
        GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
        if (clouds.Length > 0)
        {
            for (int i = 0; i < clouds.Length; i++)
            {
                GameObject.Destroy(clouds[i]);
            }
        }

        if (globalSessionScore.boxDisplayed == false)
        {
            globalSessionScore.EndSessionScore();
        }
        else
        {
            gameplayState = false;
        }
    }

	void Update() 
	{
		if (gameplayState) {

			if (!hasLaunched && !setDisplayInfo.GetIsPopupDisplayed()) {
				//Calculate launch force based on breath amount
				launchForce = breathMetre.fillAmount * breathPowerScale;

				// Launch on player action input.
				if ((Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ()) && canPlay && breathMetre.fillAmount > 0) 
				{
					CannonScoreManager scoreController = GameObject.FindObjectOfType<CannonScoreManager> ();
					scoreController.score = 0.0f;

					hasLaunched = true;
					cannonAudioSource.PlayOneShot (cannonFireClip);
					cannonSmoke.SetActive (true);
					characterAudio.PlayerLaunch ();


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
					// Apply a force to the character projectile in the direction launch vector.
					Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D> ();
					projectileRb.AddForce (projectile.transform.up * launchForce);
					//projectileRb.AddTorque (20.0f);

					StartCoroutine (GroundRespawnDelay ());
				} 
				else
				{
					// If has not been launched oscillate between the launch angle.
					AxisRotation ();
					//cannonAudioSource.enabled = true;
				}
			} 
			else if (hasLaunched) 
			{
				if (projectile.transform.position.x > 1.0f && !hasSpawnedShadow)
				{
					hasSpawnedShadow = true;
					Vector3 shadowPosition = new Vector3 (projectile.transform.position.x, -1.0f, -9.0f);
					Instantiate (shadowPrefab, shadowPosition, Quaternion.identity);
				}

				if (projectile.transform.position.y < promptHeight.position.y) 
				{
					buttonPrompt.SetActive (true);

					if (!hasSplashed) {

						Vector3 splashPosition = new Vector3 (projectile.transform.position.x + 2.0f, -0.75f, -9.0f);
						currentSplash = Instantiate (splashPrefab, splashPosition, Quaternion.identity) as GameObject;

						hasSplashed = true;
					}

					if ((Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ()) && canPlay && breathMetre.fillAmount > 0) 
					{
						breathMetre.fillAmount -= 0.1f;
						scoreManager.scoreMultiplier += 1;

						Vector2 fireDirection = new Vector2 (0, 1);

						Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D> ();
						projectileRb.velocity = new Vector2 (projectileRb.velocity.x, skimSpeed);

						int randomSkiffNum = Random.Range (0, waterSkiffs.Length - 1);
						sfxSource.PlayOneShot (waterSkiffs [randomSkiffNum]);
						characterAudio.PlaySkiff ();
					}
				} 
				else 
				{
					buttonPrompt.SetActive (false);
					hasSplashed = false;
					//if (currentSplash != null) 
					//{
						//currentSplash.SetActive (false);
					//}
				}
				
			}
		}
	}

	void PlaySplash ()
	{
		
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

	IEnumerator GroundRespawnDelay () 
	{
		yield return new WaitForSeconds (6.0f);
		if (projectile != null) 
		{
			if (projectile.transform.position.x < 2) 
			{
				GameObject.Destroy (projectile);
				Reset ();
			}
		}
	}
}
