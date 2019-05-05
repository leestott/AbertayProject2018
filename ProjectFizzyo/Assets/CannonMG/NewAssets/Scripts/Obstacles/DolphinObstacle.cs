using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinObstacle : MonoBehaviour {

	public float launchSpeed;

	Transform cameraTransform;
	GameObject projectile;
	Rigidbody2D projectileRB;

	void Start () 
	{
		cameraTransform = GameObject.FindObjectOfType<Camera> ().gameObject.transform;
	}

	void Update () 
	{
		float distanceFromCamera = cameraTransform.position.x - transform.position.x;
		if (distanceFromCamera > 20) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	void Launch() 
	{
		//Apply a positive launch force to the player
		Debug.Log ("HIT OBSTACLE");
		//A different boost is applied based on the player's downward velocity. This prevents the boost from slowing the player down when moving upward
		//While also allowing it to still be useful to prevent falling

		//If the player is moving downward faster than launch velocity
		if (projectileRB.velocity.y <= launchSpeed) 
		{
			//Apply velocity boost while directly setting y-velocity
			projectileRB.velocity = new Vector2 (projectileRB.velocity.x + launchSpeed, launchSpeed * 1.5f);
		}
		else
		{
			//Apply velocity boost normally
			projectileRB.velocity = new Vector2 (projectileRB.velocity.x + launchSpeed, projectileRB.velocity.y + launchSpeed * 1.5f);
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "CharacterProjectile") 
		{
			projectile = col.gameObject;
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
			Launch ();
		}
	}
}
