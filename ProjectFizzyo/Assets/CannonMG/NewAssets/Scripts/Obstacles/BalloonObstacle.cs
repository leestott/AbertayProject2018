using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonObstacle : MonoBehaviour {

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
		//Delete obstacle once out of view
		float distanceFromCamera = cameraTransform.position.x - transform.position.x;
		if (distanceFromCamera > 20) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	void Launch() 
	{
		//Apply boost to player based on downward y-velocity
		//Further explanation of the same method can be found in DolphinObstacle script
		Debug.Log ("HIT OBSTACLE");
		if (projectileRB.velocity.y <= launchSpeed) 
		{
			projectileRB.velocity = new Vector2 (projectileRB.velocity.x + launchSpeed, launchSpeed * 1.5f);
		}
		else
		{
			projectileRB.velocity = new Vector2 (projectileRB.velocity.x + launchSpeed, projectileRB.velocity.y + launchSpeed * 1.5f);
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		//If collided with player apply boost to player
		if (col.tag == "CharacterProjectile") 
		{
			projectile = col.gameObject;
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
			Launch ();
		}
	}
}
