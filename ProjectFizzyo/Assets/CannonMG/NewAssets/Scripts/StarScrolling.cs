using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScrolling : MonoBehaviour {

	public float speedX = 0.0f;
	public float speedY = 0.0f;
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
		transform.position = new Vector3 (camera.transform.position.x, transform.position.y, transform.position.z);

		_material.mainTextureOffset = new Vector2 (Time.time * speedX, Time.time * speedY);

		if (projectile == null) {
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
			speedX = 0.0f;
			speedY = 0.0f;
		}
		else if (projectile.transform.position.x > 2) 
		{
			Rigidbody2D rb = projectile.GetComponent<Rigidbody2D> ();
			speedX = rb.velocity.x * multiplier;
			speedY = -rb.velocity.y * multiplier;
		} 
		else 
		{
			speedX = 0.0f;
			speedY = 0.0f;
		}
	}
}
