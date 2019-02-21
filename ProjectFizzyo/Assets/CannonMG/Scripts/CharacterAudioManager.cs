using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour {

	public AudioClip[] alienSkiffs;
	public AudioClip[] alienLaunches;
	[Space]
	public AudioClip[] batBoySkiffs;
	public AudioClip[] batBoyLaunches;
	[Space]
	public AudioClip[] bearSkiffs;
	public AudioClip[] bearLaunches;
	[Space]
	public AudioClip[] unicornSkiffs;
	public AudioClip[] unicornLaunches;
	[Space]
	public AudioClip[] bigfootSkiffs;
	public AudioClip[] bigfootLaunches;

	AudioSource sfxSource;

	void Start () 
	{
		sfxSource = GameObject.Find ("SFXAudioSource").GetComponent<AudioSource> ();
	}

	public void PlaySkiff () 
	{
		int randomSkiffNumber = 0;
		switch(CannonStaticValues.playerCharacter)
		{
		case CannonStaticValues.Characters.Alien:
			randomSkiffNumber = Random.Range (0, alienSkiffs.Length - 1);
			sfxSource.PlayOneShot (alienSkiffs [randomSkiffNumber]);
			break;
		case CannonStaticValues.Characters.Batboy:
			randomSkiffNumber = Random.Range(0, batBoySkiffs.Length - 1);
			sfxSource.PlayOneShot(batBoySkiffs[randomSkiffNumber]);
			break;
		case CannonStaticValues.Characters.Bear:
			randomSkiffNumber = Random.Range(0, bearSkiffs.Length - 1);
			sfxSource.PlayOneShot(bearSkiffs[randomSkiffNumber]);
			break;
		case CannonStaticValues.Characters.Unicorn:
			randomSkiffNumber = Random.Range (0, unicornSkiffs.Length - 1);
			sfxSource.PlayOneShot (unicornSkiffs [randomSkiffNumber]);
			break;
		case CannonStaticValues.Characters.BigFoot:
			randomSkiffNumber = Random.Range (0, bigfootSkiffs.Length - 1);
			sfxSource.PlayOneShot (bigfootSkiffs [randomSkiffNumber]);
			break;
		}
	}


	public void PlayerLaunch () 
	{
		int randomLaunchNumber = 0;
		switch (CannonStaticValues.playerCharacter)
		{
		case CannonStaticValues.Characters.Alien:
			randomLaunchNumber = Random.Range (0, alienLaunches.Length - 1);
			sfxSource.PlayOneShot (alienLaunches[randomLaunchNumber]);
			break;
		case CannonStaticValues.Characters.Batboy:
			randomLaunchNumber = Random.Range(0, batBoyLaunches.Length - 1);
			sfxSource.PlayOneShot(batBoyLaunches[randomLaunchNumber]);
			break;
		case CannonStaticValues.Characters.Bear:
			randomLaunchNumber = Random.Range(0, bearLaunches.Length - 1);
			sfxSource.PlayOneShot(bearLaunches[randomLaunchNumber]);
			break;
		case CannonStaticValues.Characters.Unicorn:
			randomLaunchNumber = Random.Range (0, unicornLaunches.Length - 1);
			sfxSource.PlayOneShot (unicornLaunches [randomLaunchNumber]);
			break;
		case CannonStaticValues.Characters.BigFoot:
			randomLaunchNumber = Random.Range (0, bigfootLaunches.Length - 1);
			sfxSource.PlayOneShot (bigfootLaunches [randomLaunchNumber]);
			break;
		}
	}
}
