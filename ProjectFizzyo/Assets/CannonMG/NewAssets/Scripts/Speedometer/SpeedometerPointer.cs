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
		//BouncePointer ();
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
			//Do speed stuff
			currentSpeed = projectileRB.velocity.x;
		} 
		else 
		{
			foundProjectile = false;
			currentSpeed = 0;
		}

		currentValue =  1.0f - (maxSpeed - currentSpeed) / (maxSpeed - minSpeed);
		float currentRotation = Mathf.Lerp (minRotation, maxRotation, currentValue);

		pointerRotation = Mathf.Lerp (pointerRotation, currentRotation, Time.deltaTime);
		pointer.transform.eulerAngles = new Vector3 (0, 0, pointerRotation);

	}

	void BouncePointer () 
	{
		currentValue = Mathf.Sin (Time.time);
		currentValue = (currentValue + 1.0f) / 2.0f;
		float currentRotation = Mathf.Lerp (minRotation, maxRotation, currentValue);
		pointer.transform.eulerAngles = new Vector3 (0, 0, currentRotation);
	}
}
