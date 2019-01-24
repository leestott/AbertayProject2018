using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class ScoreController : MonoBehaviour {

	public static int highScore;
	public static float currentScore = 0;
	float originalPos = 0;
	float highestPoint = 0;
	public static GameObject toFollow;
	private bool startMeasure = false; //This is true once the Player first reaches the platform

	//send highScore at the end
	void OnApplicationQuit(){
		FizzyoFramework.Instance.Analytics.Score = highScore;
		FizzyoFramework.Instance.Achievements.PostScore (highScore);
	}
}
