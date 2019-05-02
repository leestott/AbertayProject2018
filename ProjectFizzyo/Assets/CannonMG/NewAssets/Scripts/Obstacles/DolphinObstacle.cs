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
		if (col.tag == "CharacterProjectile") 
		{
			projectile = col.gameObject;
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
			Launch ();
		}
	}
}
