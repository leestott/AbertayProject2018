using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) 
	{
		// If the character falls off the bottom of the screen reset the level.
		if (col.name == "CharacterProjectile(Clone)") 
		{
			GameObject.Destroy (col.gameObject);
		}
	}
}
