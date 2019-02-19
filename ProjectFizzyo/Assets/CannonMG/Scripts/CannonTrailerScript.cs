using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrailerScript : MonoBehaviour {

	public Sprite[] sprites;
	SpriteRenderer ren;

	int currentSprite = 0;

	void Start () 
	{
		ren = GetComponent<SpriteRenderer> ();
		ren.sprite = sprites [currentSprite];
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			currentSprite++;
			if (currentSprite >= sprites.Length) 
			{
				currentSprite = 0;
			}
			switch (currentSprite) 
			{
			case 0:
				transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				break;
			case 1:
				transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				break;
			case 2:
				transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);
				break;
			case 3:
				transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);
				break;
			}
			ren.sprite = sprites [currentSprite];
		}
	}
}
