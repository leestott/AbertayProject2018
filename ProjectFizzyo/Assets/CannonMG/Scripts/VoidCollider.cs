using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.name == "CharacterProjectile(Clone)") 
		{
			GameObject.Destroy (col.gameObject);
		}
	}
}
