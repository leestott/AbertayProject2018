using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWater : MonoBehaviour {

	public float speed = 0.0f;
	public float multiplier = 1.0f;
	private Material _material;

	public GameObject projectile;
	GameObject camera;

	void Start () 
	{
		_material = GetComponent<Renderer> ().material;
		camera = GameObject.FindObjectOfType<Camera> ().gameObject;
	}

	void Update () 
	{
		//Follwo camera on x axis
		transform.position = new Vector3 (camera.transform.position.x, transform.position.y, transform.position.z);

		//Move material offset using speed value
		_material.mainTextureOffset = new Vector2 (Time.time * speed, _material.mainTextureOffset.y);

		//Search for player projectile if null
		if (projectile == null) {
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
			speed = 0.0f;
		}
		//Set scroll speed to player speed and apply a multiplier
		else if (projectile.transform.position.x > 2) 
		{
			Rigidbody2D rb = projectile.GetComponent<Rigidbody2D> ();
			speed = rb.velocity.x * multiplier;
		} 
		else 
		{
			speed = 0.0f;
		}
	}
}
