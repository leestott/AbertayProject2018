using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class ReticleMovement : MonoBehaviour {

    // Reticle movement speed.
    [Header("Reticle movement speed:")]
    public float movementSpeed;
    // Maximum reticle range.
    [Header("The maximum reticle range:")]
    public float maxMovementRange;

    // Time the ball will take to reach coconuts.
    [Header("Travel time for the ball to reach the coconuts:")]
    public float travelTime;
	public float maskTravelTime;
	public float coconutTravelTime;
	public float maxTravelTime;

	public Transform groundPosition;

	public float DEBUG = 0.0f;

	AudioSource audioSource;
	public AudioClip maskHitEffect;
	public AudioClip groundHit;

	public AudioClip[] coconutHitEffects;

	public bool isMaskBlocking = false;
	bool hasHitGround = false;
	bool hasHitCoconut = false;

    [Header("The ball prefab:")]
    public GameObject ballPrefab;
	GameObject ballProjectile;

	public GameObject currentTarget;

	public float ballUpwardVelocity;

	public bool hasHit = false;
	bool canPlay = false;

	public float maxBallSize;
	public float minBallSize;
	public float minBallSizeFail;

	public float currentTravelTime = 0.0f;
	public bool isTravelling = false;

	BreathRecogniser br = new BreathRecogniser();
	private float breathPressure;

	private float resetDelay = 3.0f;

	public GameObject[] coconuts;

	public bool hasStartedGame = false;

	bool canReach = false;

	bool hasLaunched = false;

	ScreenShake screenShake;

	BreathMetre breathMetre;

	MinigameScoring scoreManager;

	GameObject reticle;

	public float playerBreathAmount;

	public int coconutsHit = 0;

    private GlobalSessionScore globalSessionScore;

	private SetDisplayInfo setDisplayInfo;

    void Start () 
	{
		//Get reference to breath meter instance
		breathMetre = FindObjectOfType<BreathMetre>();
		audioSource = GameObject.FindObjectOfType<AudioSource> ();
        globalSessionScore = FindObjectOfType<GlobalSessionScore>();

        br.BreathStarted += Br_BreathStarted;
		br.BreathComplete += Br_BreathComplete;

		reticle = GameObject.Find ("TargetReticle");

		screenShake = GameObject.FindObjectOfType<ScreenShake> ();

		scoreManager = GameObject.FindObjectOfType<MinigameScoring> ();

		setDisplayInfo = GameObject.FindObjectOfType<SetDisplayInfo> ();

	}

	void Update () 
	{
		DEBUG = breathMetre.fillAmount;

		if (Input.GetKeyDown(KeyCode.R))
		{
			ResetLevel ();	
		}

		if (canPlay && !setDisplayInfo.GetIsPopupDisplayed())
		{
			if (!hasLaunched) 
			{
				AxisMovement ();
			}

			if ((Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ()) && !hasLaunched && breathMetre.fillAmount > 0.1f)
			{
				if (breathMetre.fillAmount > 0.75f) 
				{
					canReach = true;
					ballUpwardVelocity = 3.0f;
				} 
				else
				{
					canReach = false;
					ballUpwardVelocity = breathMetre.fillAmount * 2.0f;
				}
				ballProjectile = Instantiate (ballPrefab, transform.position, Quaternion.identity) as GameObject;
				ballProjectile.transform.localScale = new Vector3 (maxBallSize, maxBallSize, maxBallSize);
				if (!canReach) 
				{
					SpriteRenderer ren = ballProjectile.GetComponent<SpriteRenderer> ();
					ren.sortingLayerName = "ForeGround";
					ren.sortingOrder = 2;
				}
				Rigidbody2D ballRB = ballProjectile.GetComponent<Rigidbody2D> ();
				ballRB.velocity = new Vector2 (0, ballUpwardVelocity);
				currentTravelTime = 0.0f;
				isTravelling = true;
				hasLaunched = true;	
			}

			if (hasLaunched) 
			{
				if (isTravelling && canReach) 
				{
					currentTravelTime += Time.deltaTime;
					CalculateSize ();
				}
				if (isTravelling && !canReach)
				{
                    AchievementTracker.HitInARow_Ach("Miss");
                    currentTravelTime += Time.deltaTime;
					CalculateSizeFail ();
				}
				if (currentTravelTime >= coconutTravelTime && !hasHit && canReach) 
				{
					hasHit = true;
					if (isMaskBlocking) 
					{
                        AchievementTracker.HitInARow_Ach("Mask");
                        isTravelling = false;
						currentTarget = null;
						currentTravelTime = 0.0f;
						Debug.Log ("HIT MASK");
						audioSource.PlayOneShot (maskHitEffect);
						StartCoroutine (ResetDelay ());
					} 
					else if (currentTarget != null && isTravelling) 
					{
						if (currentTarget.name == "Coconut")
						{
							isTravelling = false;
							Debug.Log ("Hit Coconut");
							if (!hasHitCoconut) 
							{
                                AchievementTracker.HitInARow_Ach("Coconut");
								hasHitCoconut = true;
								int audioNumber = Random.Range (0, coconutHitEffects.Length);
								Debug.Log ("Playing Coconut SFX: " + audioNumber);
								audioSource.PlayOneShot (coconutHitEffects [audioNumber]);
								screenShake.ShakeScreen ();
							}
							Rigidbody2D coconutRB = currentTarget.GetComponent<Rigidbody2D> ();
							coconutRB.gravityScale = 1.0f;
							float randomTorque = Random.Range (-2.0f, 2.0f);
							coconutRB.AddTorque (randomTorque);
							Animator anim = currentTarget.GetComponent<Animator> ();
							anim.SetBool ("isFalling", true);

							coconutsHit++;

							scoreManager.AddScore (50);

							if (coconutsHit >= 5)
							{
								StartCoroutine (ResetLevelDelay ());
							}
							else 
							{
								StartCoroutine (ResetDelay ());
							}
						}
					} else
                    {
                        AchievementTracker.HitInARow_Ach("Miss");
                    }
				}   

				if (currentTravelTime >= maxTravelTime) 
				{
					if (coconutsHit >= 5)
					{
						StartCoroutine (ResetLevelDelay ());
					}
					else 
					{
						StartCoroutine (ResetDelay ());
					}
				}
					//if (currentTravelTime >= travelTime && currentTarget != null) 
					//{
					//	isTravelling = false;
					//Debug.Log ("Hit Coconut");
					//Rigidbody2D coconutRB = currentTarget.GetComponent<Rigidbody2D> ();
					//coconutRB.gravityScale = 1.0f;
					//StartCoroutine (ResetDelay ());
					//} 
					//else
					//{
					//	CalculateSize ();
					//}
			}

			if (ballProjectile != null && !hasHitGround) 
			{
				if (ballProjectile.transform.position.y <= groundPosition.position.y) 
				{
					hasHitGround = true;
					//AudioSource.PlayClipAtPoint (groundHit, audioSource.transform.position);
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown () && !canPlay)
		{
			canPlay = true;
		}
	}

	public void ResetLevel() 
	{
		Reset ();
		Debug.Log ("RESET LEVEL");
		for (int i = 0; i < coconuts.Length; i++) 
		{
			Vector3 resetPosition = new Vector3 (0.0f, -0.2f, 0.0f);
			switch (i)
			{
			case 0:
				resetPosition = new Vector3 (-2.5f, -0.2f, 0);
				break;
			case 1:
				resetPosition = new Vector3 (-1.25f, -0.2f, 0);
				break;
			case 2:
				resetPosition = new Vector3 (-0.0f, -0.2f, 0);
				break;
			case 3:
				resetPosition = new Vector3 (1.25f, -0.2f, 0);
				break;
			case 4:
				resetPosition = new Vector3 (2.5f, -0.2f, 0);
				break;
			}

			Rigidbody2D currentRB = coconuts [i].GetComponent<Rigidbody2D> ();
			currentRB.gravityScale = 0.0f;
			currentRB.velocity = new Vector2 (0.0f, 0.0f);
			currentRB.angularVelocity = 0.0f;
			coconuts [i].transform.position = resetPosition;
			coconuts [i].transform.eulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
			Animator anim = coconuts[i].GetComponent<Animator> ();
			anim.SetBool ("isFalling", false);
			coconutsHit = 0;
		}
	}

    void Reset()
    {
        isTravelling = false;
        hasLaunched = false;
        currentTravelTime = 0.0f;
        hasHit = false;
        hasHitGround = false;
        hasHitCoconut = false;
        GameObject.Destroy(ballProjectile);
        breathMetre.fillAmount = 0.0f;
        breathMetre.reset = true;
		currentTarget = null;

		if (globalSessionScore != null)
		{
			if (globalSessionScore.boxDisplayed == false) 
			{
				globalSessionScore.EndSessionScore ();
			} 
			else
			{
				canPlay = false;
			}
		}
    }

	void AxisMovement ()
	{
		// Oscillate the target reticle along the X axis.
		float xPos = (maxMovementRange / 2f) * Mathf.Sin (Time.time * movementSpeed);
		transform.position = new Vector3 (xPos, transform.position.y, 0);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.name == "TikiMask")
		{
			isMaskBlocking = true;
			Debug.Log ("ENTERED MASK");
		}
		if (col.name == "Coconut")
		{
			currentTarget = col.gameObject;
		}

	}

	void CalculateSize () 
	{
		float ballScale = maxBallSize + currentTravelTime * ((minBallSize - maxBallSize) / maxTravelTime);
		ballProjectile.transform.localScale = new Vector3 (ballScale, ballScale, ballScale);
	}

	void CalculateSizeFail () 
	{
		float ballScale = maxBallSize + currentTravelTime * ((minBallSizeFail - maxBallSize) / maxTravelTime);
		ballProjectile.transform.localScale = new Vector3 (ballScale, ballScale, ballScale);
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.name == "Coconut") 
		{
			currentTarget = null;
		}
		if (col.name == "TikiMask")
		{
			isMaskBlocking = false;
		}
	}

	IEnumerator ResetDelay () 
	{
		yield return new WaitForSeconds (0.0f);
		Reset ();
	}

	IEnumerator ResetLevelDelay ()
	{
		yield return new WaitForSeconds (2.0f);
		ResetLevel ();
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
