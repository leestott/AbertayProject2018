using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	GameObject coinDeletePoint;

	void Start () 
	{
		coinDeletePoint = GameObject.Find ("CoinDeletePoint");
	}

	void Update () 
	{
		if (transform.position.x < coinDeletePoint.transform.position.x) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.tag == "CharacterProjectile")
		{
			GameObject.Destroy (this.gameObject);
		}
	}
}
