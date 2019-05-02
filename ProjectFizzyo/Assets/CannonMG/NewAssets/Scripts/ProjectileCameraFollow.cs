using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCameraFollow : MonoBehaviour {

	public Vector3 startPosition;

	private GameObject projectile;
	private bool foundCharacter;

	public float minimumCameraHeight = 0.0f;

	CannonController controller;

	GameObject camera;

	void Start () 
	{
		camera = GameObject.FindObjectOfType<Camera> ().gameObject;
		controller = GameObject.FindObjectOfType<CannonController> ();
	}

	void Update () 
	{
		projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");

		if (projectile != null) 
		{
			foundCharacter = true;
		} 
		else 
		{
			foundCharacter = false;
		}

		if (foundCharacter)
		{
			if (projectile.transform.position.x > 0)
			{
				Vector3 cameraPosition = new Vector3 (projectile.transform.position.x, projectile.transform.position.y, transform.position.z);
				if (cameraPosition.y < minimumCameraHeight) 
				{
					transform.position = new Vector3 (cameraPosition.x, minimumCameraHeight, cameraPosition.z);
				}
				else 
				{
					transform.position = cameraPosition;
				}
			} 
			else 
			{
				transform.position = startPosition;
			}
		}
	}

	public void ResetCamera() 
	{
		transform.position = startPosition;
		controller.Reset ();

	}
}
