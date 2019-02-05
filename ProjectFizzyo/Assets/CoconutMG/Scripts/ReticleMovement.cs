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

    [Header("The ball prefab:")]
    public GameObject ballPrefab;

	public GameObject currentTarget;

	BreathRecogniser br = new BreathRecogniser();
	private float breathPressure;

	private float resetDelay = 3.0f;

	public bool canPlay = false;

	BreathMetre breathMetre;

	GameObject reticle;

	public float playerBreathAmount;

	void Start () 
	{
		//Get reference to breath meter instance
		breathMetre = FindObjectOfType<BreathMetre>();

		br.BreathStarted += Br_BreathStarted;
		br.BreathComplete += Br_BreathComplete;

		reticle = GameObject.Find ("TargetReticle");
	}
		
	void Update () 
	{
		AxisMovement ();

		if (Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ())
		{
			if (currentTarget != null) 
			{
				Debug.Log ("Hit Coconut");
				Rigidbody2D coconutRB = currentTarget.GetComponent<Rigidbody2D> ();
				coconutRB.gravityScale = 1.0f;
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
		if (col.name == "Coconut") 
		{
			currentTarget = col.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		currentTarget = null;
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
