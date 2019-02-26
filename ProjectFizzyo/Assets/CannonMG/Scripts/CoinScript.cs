using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	GameObject coinDeletePoint;

	AudioSource sfxSource;
	CannonScoreManager controller;

	void Start () 
	{
		coinDeletePoint = GameObject.Find ("CoinDeletePoint");
		controller = GameObject.FindObjectOfType<CannonScoreManager> ();
		//sfxSource = GameObject.Find ("SFXAudioSource").GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (transform.position.x < coinDeletePoint.transform.position.x) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.tag == "CharacterProjectile")
		{
			//sfxSource.Play ();
			controller.coinScore += 1.0f;
            AchievementTracker.AddCoin_Ach();
			GameObject.Destroy (this.gameObject);
		}
	}
}
