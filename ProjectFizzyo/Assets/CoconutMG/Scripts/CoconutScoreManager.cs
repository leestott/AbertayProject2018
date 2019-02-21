using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoconutScoreManager : MonoBehaviour {

	Text scoreText;

	public int score;

	void Start () 
	{
		scoreText = GameObject.Find ("CoconutScoreText").GetComponent<Text> ();
	}

	void Update () 
	{
		scoreText.text = score.ToString ();
	}

}
