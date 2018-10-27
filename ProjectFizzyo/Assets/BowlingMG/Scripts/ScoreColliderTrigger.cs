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
		if (col.name == "BowlingBall") 
		{
			StartCoroutine (ScoreDelay ());
		}
	}

	void TriggerScoreManager () 
	{
		ScoreManager scoreMgr = GameObject.FindObjectOfType<ScoreManager> ();
		scoreMgr.CheckIfKnockedOver ();
	}
}
