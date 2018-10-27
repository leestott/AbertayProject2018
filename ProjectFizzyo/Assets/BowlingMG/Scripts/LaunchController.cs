using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour {

	//Represent the three stages of aiming with and integer
	int stage = 1;
	float laneWidth = 2.0f;

	[Header("Aim and Difficulty Settings")]
	public float axisMovementSpeed = 1.0f;
	public float maxRotationAngle = 90.0f;
	public float rotationSpeed = 1.0f;

	public float launchForce;
	Vector3 launchVector;

	int moveDir = 1;
	int rotateDir = 1;
	private Vector3 originalLaunchPoint;

	private GameObject launchPoint;
	private GameObject launchArrow;
	private GameObject bowlingBall;
	private Rigidbody ballRb;

	void Start () 
	{
		launchPoint = GameObject.Find ("LaunchPoint");
		launchArrow = GameObject.Find ("LaunchArrow");
		bowlingBall = GameObject.Find ("BowlingBall");

		originalLaunchPoint = launchPoint.transform.position;
		launchArrow.SetActive (false);

		ballRb = bowlingBall.GetComponent<Rigidbody> ();
	}
	  
	void Update () 
	{
		//When the player presses button 0 move onto the next aiming stage and apply the appropriate behaviour
		if (Input.GetKeyDown ("joystick 1 button 0") || Input.GetKeyDown(KeyCode.Space)) 
		{
			stage++;

			//If it is the final stage calculate the launch direction vector and normalise it
			if (stage == 3) 
			{
				launchVector = launchPoint.transform.forward;
				launchVector.Normalize ();
				//Apply a force to the ball with the appropriate direction and force
				ballRb.AddForce (launchVector * launchForce);
			}
		}

		switch (stage) 
		{
		case 1:
			//Move the launchPoint back and forth along the foul line
			AxisMovement ();
			//The ball moves with the point to give a visual representation
			bowlingBall.transform.position = launchPoint.transform.position;
			break;
		case 2:
			//Display the trajectory indicator
			launchArrow.SetActive (true);
			//Rotate the launchPoint back and forth within the maxAngle constraints
			AxisRotation ();
			break;
		}
	}

	void AxisMovement () 
	{
		if (launchPoint.transform.position.x > (originalLaunchPoint.x + laneWidth)) 
		{
			moveDir = -1;
		}
		else if (launchPoint.transform.transform.position.x < (originalLaunchPoint.x - laneWidth))
		{
			moveDir = 1;
		}

		Vector3 moveVector = new Vector3 (moveDir, 0, 0);
		launchPoint.transform.Translate (moveVector * axisMovementSpeed * Time.deltaTime);
	}

	//Rotate the launchPoint back and forth within the maxAngle constraints
	void AxisRotation ()
	{
		float angle = Mathf.PingPong (Time.time * rotationSpeed, maxRotationAngle) - (maxRotationAngle / 2f);
		launchPoint.transform.eulerAngles = new Vector3 (0, angle, 0);
	}
}
