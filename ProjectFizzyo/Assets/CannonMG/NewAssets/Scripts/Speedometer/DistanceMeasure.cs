using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeasure : MonoBehaviour {

	public float distance;

	GameObject projectile;
	bool foundProjectile = false;

	public Sprite[] numberSprites;

	public SpriteRenderer[] spriteRen;

	public int[] numbers;

	void Start () 
	{
		distance = 0;
		numbers = new int[6];
	}

	void Update() 
	{
		if (!foundProjectile)
		{
			if (GameObject.FindGameObjectWithTag("CharacterProjectile"))
			{
				projectile = GameObject.FindGameObjectWithTag("CharacterProjectile");
				foundProjectile = true;
			}
		}

		if (projectile != null)
		{
			distance = projectile.transform.position.x;
		} 
		else 
		{
			foundProjectile = false;
			distance = 0;
		}

		distance = Mathf.Round (distance);

		if (distance < 0) 
		{
			distance = 0;
		}
		if (distance > 999999)
		{
			distance = 999999;
		}
			

		int distanceInt = Mathf.RoundToInt (distance);
		string distanceString = distanceInt.ToString ("D6");

		//Debug.Log (distanceString.Length );

		for (int i = 0; i < 6; i++) 
		{
			int distanceValue = 0;
			int.TryParse (distanceString [i].ToString(), out distanceValue);
			spriteRen [i].sprite = numberSprites [distanceValue];
		}

		//Debug.Log (distanceString);
	}
}
