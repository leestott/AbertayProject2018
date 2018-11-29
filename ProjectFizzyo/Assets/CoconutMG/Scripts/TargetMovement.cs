using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour {

	//Reticle movement speed
	public float movementSpeed;
	//Maximum reticle range
	public float maxMovementRange;

	//Time the ball will take to reach coconuts
	public float travelTime;

	public GameObject ballPrefab;
	GameObject ballProjectile ;

	bool hasLaunched = false;
	bool hasHit = false;

	//Scaling and height values for ball in air
	public float maxScale = 0.4f;
	public float minScale = 0.2f;

	public float travelHeight = 0.5f;

	public float currentTravelTime;

	GameObject currentTarget;

	void Update () 
	{
		//If the ball has not been thrown move along the X axis and await player input
		if (!hasLaunched)
		{
			AxisMovement ();
			if (Input.GetKeyDown (KeyCode.Space)) {
				hasLaunched = true;
				LaunchBall ();
			}
		} 
		//If the ball has been thrown, travel until object has been hit
		else if (!hasHit)
		{
			currentTravelTime += Time.deltaTime;
			BallTravel ();
		}
	}

	void AxisMovement ()
	{
		//Oscillate the target reticle along the X axis
		float xPos = (maxMovementRange / 2f) * Mathf.Sin (Time.time * movementSpeed);
		transform.position = new Vector3 (xPos, transform.position.y, 0);
	}

	void LaunchBall () 
	{
		//Hide the target reticle once launched
		GameObject reticle = GameObject.Find ("TargetReticle");
		if (reticle != null)
		{
			reticle.SetActive (false);
		}
		//Instantiate ball from saved prefab
		ballProjectile = Instantiate (ballPrefab, transform.position, Quaternion.identity);
		//Reset the balls current travel time
		currentTravelTime = 0;
	}

	void BallTravel () 
	{
		//If the ball is currently in the air but has not hit an object
		if (currentTravelTime < travelTime) 
		{
			//Scale the ball down over the course of its travel to imitate distance
			float scaleValue = Mathf.Lerp (maxScale, minScale, currentTravelTime / travelTime);
			ballProjectile.transform.localScale = new Vector3 (scaleValue, scaleValue, scaleValue);

			//Move the ball up and back down to the target over the travel time to imitate a thrown object while maintaining the accuracy of the reticle
			float height = 2.5f + (travelHeight * Mathf.Sin ((currentTravelTime / travelTime) * 180 * Mathf.Deg2Rad));
			ballProjectile.transform.position = new Vector2 (transform.position.x, height);
		}
		else 
		{
			//If the ball has hit then apply gravity to it
			hasHit = true;
			ballProjectile.GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
			if (currentTarget != null) 
			{
				//If the ball has hit a coconut then apply gravity to the hit coconut
				currentTarget.GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		//If the reticle is over a coconut, set it as the current target
		//This is how the ball hit is detected upon launch
		currentTarget = col.gameObject;
		Debug.Log ("Current Target Name: " + currentTarget.name);
	}

	void OnTriggerExit2D (Collider2D col) 
	{
		currentTarget = null;
	}
}
