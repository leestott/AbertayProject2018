using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	public float shakeDuration = 0.5f;
	float shake = 0.0f;
	public float shakeMagnitude = 0.15f;
	float dampingSpeed = 1.0f;

	Vector3 originalPosition;

	void Start () 
	{
		originalPosition = transform.position;	
	}

	void Update () 
	{
		if (shake > 0)
		{
			Vector2 shakeValue = Random.insideUnitCircle * shakeMagnitude;
			transform.localPosition = new Vector3 (shakeValue.x, shakeValue.y, transform.localPosition.z);
			shake -= Time.deltaTime * dampingSpeed;
		}
		else 
		{
			shake = 0.0f;
			transform.position = originalPosition;
		}
	}

	public void ShakeScreen () 
	{
		shake = shakeDuration;
	}
}
