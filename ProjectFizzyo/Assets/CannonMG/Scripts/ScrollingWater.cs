using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWater : MonoBehaviour {

	public float speed = 0.0f;
	public float multiplier = 1.0f;
	private Material _material;
	void Start () {
		_material = GetComponent<Renderer> ().material;
	}

	void Update () {
		_material.mainTextureOffset = new Vector2 (Time.time * speed, 0);
		//GetComponent<Renderer>().material.mainTextureOffset = offset;

		if (GameObject.FindGameObjectWithTag ("CharacterProjectile") != null) {
			if (GameObject.FindGameObjectWithTag ("CharacterProjectile").transform.position.x > 2)
			{
				Rigidbody2D rb = GameObject.FindGameObjectWithTag ("CharacterProjectile").GetComponent<Rigidbody2D> ();
				float rbSpeed = rb.velocity.x * multiplier;
				speed = rbSpeed;
			}
			else 
			{
				speed = 0.0f;
			}
		}
		else 
		{
			speed = 0.0f;
		}
	}

}
