using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

	public float rotationSpeed;
	public float maxRotationAngle;
	public float launchForce;

	public GameObject characterProjectile;
	public GameObject playerCamera;

	bool hasLaunched = false;

	void Update() 
	{
		if (!hasLaunched)
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				hasLaunched = true;
				GameObject launchDir = GameObject.Find ("LaunchDirection");
				GameObject projectile = Instantiate (characterProjectile, transform.position, launchDir.transform.rotation);
				Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D> ();
				projectileRb.AddForce (projectile.transform.up * launchForce);
			} 
			else 
			{
				AxisRotation ();
			}
		}
	}

	void AxisRotation ()
	{
		float angle = Mathf.PingPong (Time.time * rotationSpeed, maxRotationAngle) - (maxRotationAngle / 2f);
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
}
