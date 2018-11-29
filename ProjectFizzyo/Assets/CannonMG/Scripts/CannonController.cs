using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

    [Header("Rotation speed and angle of cannon:")]
    public float rotationSpeed;
	public float maxRotationAngle;

    // Public variable for testing launch force.
    [Header("Cannons force:")]
    public float launchForce;

    // The current character prefab.
    [Header("Current character prefab:")]
    public GameObject characterProjectile;

    // The camera which follows the character.
    [Header("Camera object:")]
    public GameObject playerCamera;

	bool hasLaunched = false;

	void Update() 
	{
		if (!hasLaunched)
		{
			// Launch on player action input.
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				hasLaunched = true;

				// Calculate launch vector.
				GameObject launchDir = GameObject.Find ("LaunchDirection");
				GameObject projectile = Instantiate (characterProjectile, transform.position, launchDir.transform.rotation);

				// Apply a force to the character projectile in the direction launch vector.
				Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D> ();
				projectileRb.AddForce (projectile.transform.up * launchForce);
			} 
			else 
			{
				// If has not been launched oscillate between the launch angle.
				AxisRotation ();
			}
		}
	}

	void AxisRotation ()
	{
		// Oscillate between the max and min angles and a set speed.
		float angle = Mathf.PingPong (Time.time * rotationSpeed, maxRotationAngle) - (maxRotationAngle / 2f);
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
}
