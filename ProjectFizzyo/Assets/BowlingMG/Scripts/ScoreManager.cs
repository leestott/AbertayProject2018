using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public float minScoreAngle = 15;
	public GameObject[] pins;

	void Start () 
	{
		pins = GameObject.FindGameObjectsWithTag ("BowlingPins");
	}

	public void CheckIfKnockedOver () 
	{
		int pinsKnockedOver = 0;

		//Detect if pins have been knocked past the minScoreAngle from upright
		for (int i = 0; i < pins.Length; i++) 
		{
			float theta = Mathf.Acos(Vector3.Dot (Vector3.up, pins [i].transform.up)) * Mathf.Rad2Deg;
			if (theta > minScoreAngle) 
			{
				//If so destroy the pin and increment hit counter
				GameObject.Destroy (pins [i]);
				pinsKnockedOver++;
			}
		}
	}
}
