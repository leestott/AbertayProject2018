using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTracker : MonoBehaviour {

	GameObject characterProjectile;

	void Start () 
	{
		characterProjectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
	}

	void Update () 
	{
		if (characterProjectile != null) 
		{
			//Vector3 position = new Vector3 (characterProjectile.transform.position.x, -1.0f, transform.position.z);
			//transform.position = position;
			if (transform.position.x < characterProjectile.transform.position.x - 10.0f)
			{
				GameObject.Destroy (this.gameObject);
			}
		}

	}
}
