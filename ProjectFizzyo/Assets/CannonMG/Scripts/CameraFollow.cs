using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Starting position of the camera.
    [Header("Cameras position:")]
    public Vector3 startPosition = new Vector3 (0.0f, 2.0f, -10.0f);

    // Which character is being fired prefab.
    private GameObject characterProjectile;

    // Debug value for if the character has been found.
    private bool foundCharacter;

	void Update () 
	{
		// Find launched character.
		characterProjectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");

		if (characterProjectile != null) 
		{
			foundCharacter = true;
		} 
		else 
		{
			// If the character cannot be found stay at or reset to start position.
			foundCharacter = false;
			transform.position = startPosition;
		}

		if (foundCharacter) 
		{
			// Follow the character while in air.
			transform.position = new Vector3 (characterProjectile.transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
