using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrailerScript : MonoBehaviour {

	public Sprite[] sprites;
	SpriteRenderer ren;

	int currentSprite = 0;

	Vector3 originalRotation;
	Vector3 unicornRotation = new Vector3(0.0f, 0.0f, 0.0f);

	void Start () 
	{
		ren = GetComponent<SpriteRenderer> ();
		ren.sprite = sprites [currentSprite];
		originalRotation = transform.rotation.eulerAngles;

		StartCoroutine (ChangeDelay ());
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			NewSprite ();
		}
	}

	IEnumerator ChangeDelay () 
	{
		yield return new WaitForSeconds (0.25f);
		NewSprite ();
	}

	void NewSprite () 
	{
		currentSprite++;
		if (currentSprite >= sprites.Length) 
		{
			currentSprite = 0;
		}
		switch (currentSprite) 
		{
		case 0:
			transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);
			break;
		case 1:
			transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			break;
		case 2:
			transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			break;
		case 3:
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			transform.eulerAngles = unicornRotation;
			break;
		case 4:
			transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			transform.eulerAngles = originalRotation;
			break;
		}
		ren.sprite = sprites [currentSprite];
		StartCoroutine (ChangeDelay ());
	}
}

