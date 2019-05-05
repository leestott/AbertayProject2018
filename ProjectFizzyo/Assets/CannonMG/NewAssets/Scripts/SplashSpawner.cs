using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSpawner : MonoBehaviour {

	public GameObject splashPrefab;
	GameObject currentSplash;

	void OnCollisionEnter2D (Collision2D col) 
	{
		//If the player has bounced of the water spawn a splash sprite
		if (col.collider.tag == "CharacterProjectile") 
		{
			Debug.Log ("BOUNCE!");
			Vector3 splashPosition = new Vector3 (col.transform.position.x, -3.0f, -9.0f);
			currentSplash = Instantiate (splashPrefab, splashPosition, Quaternion.identity) as GameObject;
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		//If the player has splashed through the water spawn a splash sprite
		if (col.tag == "CharacterProjectile") 
		{
			Debug.Log ("SPLASH!");
			Vector3 splashPosition = new Vector3 (col.transform.position.x, -3.0f, -9.0f);
			currentSplash = Instantiate (splashPrefab, splashPosition, Quaternion.identity) as GameObject;
		}
	}
}
