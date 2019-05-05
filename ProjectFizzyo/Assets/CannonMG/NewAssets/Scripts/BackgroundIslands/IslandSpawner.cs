using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour {

	// Global and public variables
	public GameObject[] islands;

	public float minDelay;
	public float maxDelay;

	public bool hasFound = false;
	bool hasSpawned = false;

	GameObject projectile;

	int previousIndex = 0;

	void Update () 
	{
		//Find player projectile
		if (!hasFound)
		{
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
		}
		else 
		{
			if (!hasSpawned)
			{
				hasSpawned = true;
				SpawnIsland ();
			}
		}

		if (projectile != null) 
		{
			hasFound = true;
		} 
		else 
		{
			hasFound = false;
			hasSpawned = false;
		}
	}

	void SpawnIsland () 
	{
		//If the projectile is not null
		if (hasFound && projectile != null) 
		{
			Debug.Log ("SPAWNING ISLAND");

			int randomSpawnIndex = 0;

			//Calculate a new random island index but repeat until it's not the same as the previous
			//This prevents multiple of the same island being repeated
			do 
			{
				randomSpawnIndex = Random.Range (0, islands.Length);
			} while (randomSpawnIndex == previousIndex);

			previousIndex = randomSpawnIndex;

			//Create a vector3 infront of the camera in the x axis to spawn the new island
			Vector3 spawnPosition = new Vector3 (Camera.main.transform.position.x + 20.0f, islands [randomSpawnIndex].transform.position.y, islands [randomSpawnIndex].transform.position.z);
			//Instantiate new island at spawn postition and initialse the trackingg script for the new island
			GameObject currentIsland = Instantiate (islands [randomSpawnIndex], spawnPosition, Quaternion.identity) as GameObject;
			IslandTracking islandScript = currentIsland.GetComponent<IslandTracking> ();
			//Pass the new island a reference to the projectile
			islandScript.InitializeIsland (projectile);

			StartCoroutine (SpawnDelay ());
		}
	}

	IEnumerator SpawnDelay () 
	{
		//Wait a random time between a min and max before spawning a new island
		float randomDelay = Random.Range (minDelay, maxDelay);
		yield return new WaitForSeconds (randomDelay);
		SpawnIsland ();
	}
}
