using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreColliderTrigger : MonoBehaviour {

	public float scoreDelayTime = 2.5f;

	IEnumerator ScoreDelay () 
	{
		yield return new WaitForSeconds (scoreDelayTime);
		TriggerScoreManager ();
	}

	void OnTriggerEnter (Collider col) 
	{
		//Once the bowling ball has entered the pin area wait a delayTime before scoring
		if (col.name == "BowlingBall") 
		{
			StartCoroutine (ScoreDelay ());
		}
	}

	void TriggerScoreManager () 
	{
		//Once delay time is up call scoring script
		ScoreManager scoreMgr = GameObject.FindObjectOfType<ScoreManager> ();
		scoreMgr.CheckIfKnockedOver ();
	}
}
