using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColourChange : MonoBehaviour {

	public Color minColour;
	public Color maxColour;

	public float minHeight;
	public float maxHeight;

	public float currentHeightScaled = 0;

	private Material skyMaterial;
	private GameObject camera;

	void Start () 
	{
		skyMaterial =  GetComponent<Renderer> ().material;
		camera = GameObject.FindObjectOfType<Camera> ().gameObject;
	}

	void Update () 
	{
		float cameraHeight = camera.transform.position.y;

		currentHeightScaled = (cameraHeight - minHeight) / (maxHeight - minHeight);

		Color currentColour = Color.Lerp (minColour, maxColour, currentHeightScaled);
		skyMaterial.color = currentColour;
	}
}
