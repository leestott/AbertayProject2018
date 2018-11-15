using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Vector3 startPosition = new Vector3 (0.0f, 2.0f, -10.0f);
	public bool foundCharacter;

	public GameObject characterProjectile;

	void Update () 
	{
		characterProjectile = GameObject.Find ("CharacterProjectile(Clone)");
		if (characterProjectile != null) 
		{
			foundCharacter = true;
		} 
		else 
		{
			foundCharacter = false;
			transform.position = startPosition;
		}

		if (foundCharacter) 
		{
			transform.position = new Vector3 (characterProjectile.transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
