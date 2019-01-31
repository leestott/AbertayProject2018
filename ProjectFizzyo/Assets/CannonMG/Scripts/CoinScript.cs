using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	GameObject coinDeletePoint;

	AudioSource sfxSource;

	void Start () 
	{
		coinDeletePoint = GameObject.Find ("CoinDeletePoint");
		sfxSource = GameObject.Find ("SFXAudioSource").GetComponent<AudioSource> ();
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
			sfxSource.Play ();

			GameObject.Destroy (this.gameObject);
		}
	}
}
