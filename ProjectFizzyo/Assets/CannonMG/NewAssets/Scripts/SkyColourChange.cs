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

		//Normalize height between range of 0 and 1 using max min range for height values
		currentHeightScaled = (cameraHeight - minHeight) / (maxHeight - minHeight);

		//Lerp colour between min and max using normalized height value
		Color currentColour = Color.Lerp (minColour, maxColour, currentHeightScaled);
		skyMaterial.color = currentColour;
	}
}
