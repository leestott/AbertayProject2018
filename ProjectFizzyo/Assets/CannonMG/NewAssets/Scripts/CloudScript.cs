using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

	void Update () 
	{
		if (Camera.main.transform.position.x - transform.position.x > 16) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}
}
