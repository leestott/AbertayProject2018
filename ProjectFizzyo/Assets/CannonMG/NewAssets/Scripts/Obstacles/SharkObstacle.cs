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
		float distanceFromCamera = cameraTransform.position.x - transform.position.x;
		if (distanceFromCamera > 20) 
		{
			GameObject.Destroy (this.gameObject);
		}

		if (Input.GetKeyDown (KeyCode.T))
		{
			SpawnHead ();
		}
	}

	void Catch() 
	{
		Debug.Log ("CAUGHT BY SHARK");
		projectileRB.velocity = new Vector2 (0, 0);
		projectile.transform.position = transform.position;
		GameObject.Destroy (projectile);
		SpawnHead ();
		StartCoroutine (RespawnDelay ());
	}

	void SpawnHead() 
	{
		sharkFin.SetActive (false);
		sharkHead.SetActive (true);
		waterSplash.SetActive (true);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "CharacterProjectile") 
		{
			projectile = col.gameObject;
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
			Catch ();
		}
	}

	IEnumerator RespawnDelay() 
	{
		yield return new WaitForSeconds (2.5f);
		cameraScript.ResetCamera ();
	}
}
