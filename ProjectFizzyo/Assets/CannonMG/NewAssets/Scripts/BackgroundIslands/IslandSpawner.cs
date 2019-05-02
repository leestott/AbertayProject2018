using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour {

	public GameObject[] islands;

	public float minDelay;
	public float maxDelay;

	public bool hasFound = false;
	bool hasSpawned = false;

	GameObject projectile;

	int previousIndex = 0;

	void Update () 
	{
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
		if (hasFound && projectile != null) 
		{
			Debug.Log ("SPAWNING ISLAND");

			int randomSpawnIndex = 0;

			do 
			{
				randomSpawnIndex = Random.Range (0, islands.Length);
			} while (randomSpawnIndex == previousIndex);

			previousIndex = randomSpawnIndex;

			Vector3 spawnPosition = new Vector3 (Camera.main.transform.position.x + 20.0f, islands [randomSpawnIndex].transform.position.y, islands [randomSpawnIndex].transform.position.z);
			GameObject currentIsland = Instantiate (islands [randomSpawnIndex], spawnPosition, Quaternion.identity) as GameObject;
			IslandTracking islandScript = currentIsland.GetComponent<IslandTracking> ();
			islandScript.InitializeIsland (projectile);

			StartCoroutine (SpawnDelay ());
		}
	}

	IEnumerator SpawnDelay () 
	{
		float randomDelay = Random.Range (minDelay, maxDelay);
		yield return new WaitForSeconds (randomDelay);
		SpawnIsland ();
	}
}
