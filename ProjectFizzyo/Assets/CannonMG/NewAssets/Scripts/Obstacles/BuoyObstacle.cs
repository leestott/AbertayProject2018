using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyObstacle : MonoBehaviour {

	private GameObject projectile;
	private Rigidbody2D projectileRB;

	public float forceMultplier;

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "CharacterProjectile") {
			projectile = col.gameObject;
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
			ApplySlowForce ();
		}
	}

	void ApplySlowForce () 
	{
		projectileRB.AddForce (Vector2.right * forceMultplier * projectileRB.velocity.x * -1.0f);
	}
}
