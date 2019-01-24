using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

	Transform cloudDeletePoint;

	void Start () 
	{
		cloudDeletePoint = GameObject.Find ("CloudDeletePoint").transform;
	}

	void Update ()
	{
		if (transform.position.x < cloudDeletePoint.position.x) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}
}
