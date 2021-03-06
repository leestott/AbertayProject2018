﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTracking : MonoBehaviour {

	private Rigidbody2D rb;

	private Rigidbody2D projectileRB;

	public float speedMultiplier;

	GameObject projectile;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	public void InitializeIsland (GameObject player)
	{
		projectile = player;
		projectileRB = projectile.GetComponent<Rigidbody2D> ();
	}

	void Update () 
	{
		//Set island velocity to a percentage of the projectile velocity to create a parallax effect
		if (projectile != null && projectileRB != null) 
		{
			rb.velocity = new Vector2 (projectileRB.velocity.x * speedMultiplier, 0.0f);
		} 
		else
		{
			//If projectile is null the stop moving
			rb.velocity = new Vector2 (0.0f, 0.0f);
		}

		//Destroy island once off screen
		if (Camera.main.transform.position.x - transform.position.x > 16) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}
}
