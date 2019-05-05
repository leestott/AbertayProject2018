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

	CharacterAudioManager characterAudioManager;

	void Start () 
	{
		_material = GetComponent<Renderer> ().material;
		camera = GameObject.FindObjectOfType<Camera> ().gameObject;
		box = GetComponent<BoxCollider2D> ();
		controller = GameObject.FindObjectOfType<CannonController> ();
		characterAudioManager = GameObject.FindObjectOfType<CharacterAudioManager> ();
	}

	void Update () 
	{
		//Uses the same material scrolling method as ScrollingWater script
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

		//Once game over, disable boxcollider to allow player to fall through
		if (controller.gameOver)
		{
			box.isTrigger = true;
		} 
		else 
		{
			box.isTrigger = false;
		}
	}

	void OnCollisionEnter2D (Collision2D collision) 
	{
		if (collision.collider.tag == "CharacterProjectile") 
		{
			characterAudioManager.PlaySkiff ();
		}		
	}
}
