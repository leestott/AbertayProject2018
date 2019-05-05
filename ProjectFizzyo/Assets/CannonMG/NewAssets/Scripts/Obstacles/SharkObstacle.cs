using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkObstacle : MonoBehaviour {

	Transform cameraTransform;
	GameObject projectile;
	Rigidbody2D projectileRB;

	GameObject sharkFin;
	GameObject sharkHead;
	GameObject waterSplash;

	ProjectileCameraFollow cameraScript;

	void Start () 
	{
		cameraTransform = GameObject.FindObjectOfType<Camera> ().gameObject.transform;
		cameraScript = GameObject.FindObjectOfType<ProjectileCameraFollow> ();
		//Get references to all child sprites of shark
		foreach (Transform child in transform)
		{
			if (child.name == "SharkFin") 
			{
				sharkFin = child.gameObject;	
			} 
			else if (child.name == "SharkHead") 
			{
				sharkHead = child.gameObject;
			} 
			else if (child.name == "WaterSplash") 
			{
				waterSplash = child.gameObject;
			} 
		}

		sharkFin.SetActive (true);
		sharkHead.SetActive (false);
		waterSplash.SetActive (false);
	}

	void Update () 
	{
		//Calculate distance from the camera on x axis
		float distanceFromCamera = cameraTransform.position.x - transform.position.x;
		//Destroy once out of view
		if (distanceFromCamera > 20) 
		{
			GameObject.Destroy (this.gameObject);
		}

		//Debug controls for triggering caught animation
		if (Input.GetKeyDown (KeyCode.T))
		{
			SpawnHead ();
		}
	}

	void Catch() 
	{
		//If caught by shark stop player projectile and destroy
		Debug.Log ("CAUGHT BY SHARK");
		projectileRB.velocity = new Vector2 (0, 0);
		projectile.transform.position = transform.position;
		GameObject.Destroy (projectile);
		SpawnHead ();
		StartCoroutine (RespawnDelay ());
	}

	//Spawn shark head and splash once been hit
	void SpawnHead() 
	{
		sharkFin.SetActive (false);
		sharkHead.SetActive (true);
		waterSplash.SetActive (true);
	}

	//Detect if collision was with a player
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "CharacterProjectile") 
		{
			projectile = col.gameObject;
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
			//Catch the player projectile
			Catch ();
		}
	}

	IEnumerator RespawnDelay() 
	{
		//Reset camera position after delay
		yield return new WaitForSeconds (2.5f);
		cameraScript.ResetCamera ();
	}
}
