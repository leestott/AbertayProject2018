using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spins the loading icon as it fills
public class LoadingIconSpin : MonoBehaviour {

	public float rotationSpeed = 0.0f;

	RectTransform rectTransform;

	void Start () 
	{
		rectTransform = GetComponent<RectTransform> ();
	}

	void Update () 
	{
		rectTransform.Rotate (new Vector3 (0.0f, 0.0f, rotationSpeed * Time.deltaTime));
	}
}
