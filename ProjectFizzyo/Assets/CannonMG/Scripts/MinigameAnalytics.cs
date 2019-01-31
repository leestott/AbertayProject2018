using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameAnalytics : MonoBehaviour {

	public string minigameName;

	public float gameTime = 0.0f;
	public int breaths = 0;

	void Start () 
	{
		
	}

	void Update () 
	{
		gameTime += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Debug.Log ("Exit Game");
			//AnalyticsManager.ReportEndOfMinigame(minigameName, gameTime, breaths);
		}
	}

	void OnDestroy () 
	{
		Debug.Log ("On Destroy Exit");
		AnalyticsManager.ReportEndOfMinigame(minigameName, gameTime, breaths);
	}
}
