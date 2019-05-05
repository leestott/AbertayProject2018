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
		//Find player projectile
		if (projectile != null) 
		{
			foundCharacter = true;
		} 
		else 
		{
			foundCharacter = false;
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
		}

		if (foundCharacter)
		{
			//Track player position onces passed a minimum x value to prevent camera snapping to a new position on cannon fire
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

	//Reset camera position
	public void ResetCamera() 
	{
		transform.position = startPosition;
		controller.Reset ();

	}
}
