using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTracker : MonoBehaviour {

	GameObject characterProjectile;

	public float maxScale = 2.0f;
	public float minScale = 0.1f;

	public float scale;

	void Start () 
	{
		characterProjectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
	}

	void Update () 
	{
		//If player exists and is above the water
		if (characterProjectile != null && characterProjectile.transform.position.y > transform.position.y - 1.0f)
		{
			//Follow player position on x axis
			Vector3 position = new Vector3 (characterProjectile.transform.position.x, transform.position.y, transform.position.z);
			transform.position = position;

			float height = characterProjectile.transform.position.y;

			//Adjust shadow scale using players height
			scale = (-maxScale / 10.0f) * characterProjectile.transform.position.y + maxScale;
			if (scale < minScale) 
			{
				scale = minScale;
			} 
			else if (scale > maxScale) 
			{
				scale = maxScale;
			}
			transform.localScale = new Vector3 (scale, scale, scale);

		}
		else 
		{
			//Destroy shadow if the player is below the water or has been destroyed
			GameObject.Destroy (this.gameObject);
		}
	}
}
