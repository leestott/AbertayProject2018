using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSessionScore : MonoBehaviour {

	public int goodBreaths = 0;
	public int totalBreaths = 0;

	public int qualityScore = 0;

	void Start () 
	{
		DontDestroyOnLoad (this);
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			goodBreaths = Random.Range (5, 12);
			totalBreaths = Random.Range (8, 12);
			if (goodBreaths > totalBreaths) 
			{
				goodBreaths = totalBreaths;
			}
			CalculateScore ();
		}
	}

	void CalculateScore () 
	{
		float qualityPercentage = (float)goodBreaths / (float)totalBreaths;
		qualityScore = ((int)(qualityPercentage * 100)) + Random.Range (0, 9);
		if (qualityPercentage < 0.4f)
		{
			qualityScore = 40 + Random.Range (0, 9);
		}
		if (qualityScore > 100) 
		{
			qualityScore = 100;
		}

		Debug.Log ("QUALITY SCORE: " + qualityScore);
	}


}
