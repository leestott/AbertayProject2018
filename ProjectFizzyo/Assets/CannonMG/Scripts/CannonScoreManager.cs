using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class CannonScoreManager : MonoBehaviour {

	float score = 0.0f;
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
		if (GameObject.Find ("CharacterProjectile(Clone)") != null)
		{
			GameObject projectile = GameObject.Find ("CharacterProjectile(Clone)");
			score = projectile.transform.position.x - startPosition;
			score *= scoreMultiplier;
			scoreText.text = Mathf.RoundToInt (score).ToString ();
		}
	}
}
