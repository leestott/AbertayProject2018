using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedometerPointer : MonoBehaviour {

	GameObject pointer;

	public float currentSpeed;
	public float minSpeed;
	public float maxSpeed;

	public float minRotation;
	public float maxRotation;

	public float currentValue;

	GameObject projectile;
	Rigidbody2D projectileRB;
	bool foundProjectile = false;

	float pointerRotation = 0;

	void Start () 
	{
		//Get reference to pointer sprite
		foreach (Transform child in transform)
		{
			if (child.name == "Pointer") 
			{
				pointer = child.gameObject;
			}
		}
	}

	void Update () 
	{
		//Find player projectile
		if (!foundProjectile)
		{
			if (GameObject.FindGameObjectWithTag("CharacterProjectile"))
			{
				projectile = GameObject.FindGameObjectWithTag("CharacterProjectile");
				projectileRB = projectile.GetComponent<Rigidbody2D> ();
				foundProjectile = true;
			}
		}

		if (projectile != null)
		{
			currentSpeed = projectileRB.velocity.x;
		} 
		else 
		{
			foundProjectile = false;
			currentSpeed = 0;
		}

		//Normalize x velocity between 0 and 1 using a max min range
		currentValue =  1.0f - (maxSpeed - currentSpeed) / (maxSpeed - minSpeed);
		float currentRotation = Mathf.Lerp (minRotation, maxRotation, currentValue);

		//Set pointer rotation to the calculated rotation
		pointerRotation = Mathf.Lerp (pointerRotation, currentRotation, Time.deltaTime);
		pointer.transform.eulerAngles = new Vector3 (0, 0, pointerRotation);

	}

	//Debug script to bounce pointer back and forth between min a max rotation
	//Kept as it could be used for an interesting effect
	void BouncePointer () 
	{
		currentValue = Mathf.Sin (Time.time);
		currentValue = (currentValue + 1.0f) / 2.0f;
		float currentRotation = Mathf.Lerp (minRotation, maxRotation, currentValue);
		pointer.transform.eulerAngles = new Vector3 (0, 0, currentRotation);
	}
}
