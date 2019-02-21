using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class CannonScoreManager : MonoBehaviour {

	public float score = 0.0f;
	public float coinScore = 0.0f;
	public int scoreMultiplier = 1;

	public float startPosition;

	GameObject projectile;
	GameObject cannonBody;

	Text scoreText;

	void Start () 
	{
		cannonBody = GameObject.Find ("CannonBody");
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		startPosition = cannonBody.transform.position.x;
	}

	void Update () 
	{
		if (GameObject.FindGameObjectWithTag("CharacterProjectile") != null)
		{
			GameObject projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
			score = (projectile.transform.position.x - startPosition) / 10.0f;
			score += coinScore;
			scoreText.text = Mathf.RoundToInt (score).ToString ();
		}
	}
}
