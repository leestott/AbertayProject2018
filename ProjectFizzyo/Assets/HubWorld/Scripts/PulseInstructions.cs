using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseInstructions : MonoBehaviour {

	// Starting value for the Lerp.
	static float t = 0.0f;
	// Animate the game object to scale from minimum to maximum and back.
	private float minimum = 0.0F;
	private float maximum = 0.1F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

			// Increase the t interpolater for lerp using deltatime.
			t += 1.0f * Time.deltaTime;

			// When interpolater reaches one swap max and min to scale other way.
			if (t > 1.0f)
			{
				float temp = maximum;
				maximum = minimum;
				minimum = temp;
				t = 0.0f;
			}

			// Scale based on lerp.
			transform.localScale = new Vector3(1 + Mathf.Lerp(minimum, maximum, t), 1 + Mathf.Lerp(minimum, maximum, t), 1 + Mathf.Lerp(minimum, maximum, t));
		}
	}

