using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWaterFront : MonoBehaviour {

	public float speed = 0.0f;
	public float multiplier = 1.0f;
	private Material _material;

	public GameObject projectile;
	GameObject camera;

	BoxCollider2D box;
	CannonController controller;

	void Start () 
	{
		_material = GetComponent<Renderer> ().material;
		camera = GameObject.FindObjectOfType<Camera> ().gameObject;
		box = GetComponent<BoxCollider2D> ();
		controller = GameObject.FindObjectOfType<CannonController> ();
	}

	void Update () 
	{
		transform.position = new Vector3 (camera.transform.position.x, transform.position.y, transform.position.z);

		_material.mainTextureOffset = new Vector2 (Time.time * speed, _material.mainTextureOffset.y);

		if (projectile == null) {
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
			speed = 0.0f;
		}
		else if (projectile.transform.position.x > 2) 
		{
			Rigidbody2D rb = projectile.GetComponent<Rigidbody2D> ();
			speed = rb.velocity.x * multiplier;
		} 
		else 
		{
			speed = 0.0f;
		}

		if (controller.gameOver)
		{
			box.isTrigger = true;
		} 
		else 
		{
			box.isTrigger = false;
		}
	}
}
