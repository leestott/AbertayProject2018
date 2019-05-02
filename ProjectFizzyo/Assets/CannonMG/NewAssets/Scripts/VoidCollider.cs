using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {

	CannonController controller;
	ProjectileCameraFollow cameraScript;

	void Start () 
	{
		controller = GameObject.FindObjectOfType<CannonController> ();
		cameraScript = GameObject.FindObjectOfType<ProjectileCameraFollow> ();
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "CharacterProjectile") 
		{
			controller.Reset ();
			GameObject.Destroy (col.gameObject);
			StartCoroutine (RespawnDelay ());
		}
	}

	IEnumerator RespawnDelay() 
	{
		yield return new WaitForSeconds (2.5f);
		cameraScript.ResetCamera ();
	}
}
