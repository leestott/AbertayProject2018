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

		for (int i = 0; i < pins.Length; i++) 
		{
			float theta = Mathf.Acos(Vector3.Dot (Vector3.up, pins [i].transform.up)) * Mathf.Rad2Deg;
			if (theta > minScoreAngle) 
			{
				GameObject.Destroy (pins [i]);
				pinsKnockedOver++;
			}
			Debug.Log ("Pin Number " + i + ": " + theta);
		}

		Debug.Log ("Total Pins Knocked Over: " + pinsKnockedOver);
	}
}
