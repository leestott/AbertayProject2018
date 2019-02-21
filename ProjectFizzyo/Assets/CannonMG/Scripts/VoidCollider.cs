using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {

	AudioSource sfxSource;
	public AudioClip waterSplash;

	void Start () 
	{
		sfxSource = GameObject.Find ("SFXAudioSource").GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		// If the character falls off the bottom of the screen reset the level.
		if (col.tag == "CharacterProjectile") 
		{
			sfxSource.PlayOneShot (waterSplash);
			GameObject.Destroy (col.gameObject);
			CannonController controller = GameObject.FindObjectOfType<CannonController> ();
			controller.Reset ();
		}
	}
}
