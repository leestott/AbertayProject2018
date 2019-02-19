using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTracking : MonoBehaviour {

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
		if (characterProjectile != null && characterProjectile.transform.position.y > transform.position.y - 1.0f)
		{
			Vector3 position = new Vector3 (characterProjectile.transform.position.x, transform.position.y, transform.position.z);
			transform.position = position;

			float height = characterProjectile.transform.position.y;

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
			GameObject.Destroy (this.gameObject);
		}
	}

}
