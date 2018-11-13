using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour {

	private Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		transform.rotation = Quaternion.LookRotation (Vector3.forward, rb.velocity);
	}
}
