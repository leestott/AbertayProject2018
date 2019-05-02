using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	public GameObject[] clouds;
	public GameObject seagullsPrefab;
	public GameObject starPrefab;

	public float minDelay;
	public float maxDelay;

	public float minHeight;
	public float maxHeight;

	Transform spawnPoint;
	GameObject projectile;

	GameObject camera;

	public bool hasFound = false;
	public bool hasSpawned = false;

	void Start () 
	{
		spawnPoint = GameObject.Find ("CloudSpawnPoint").transform;
		camera = GameObject.FindObjectOfType<Camera> ().gameObject;
	}

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
				SpawnCloud ();
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

	void SpawnCloud()
	{
		if (hasFound && projectile != null) 
		{
			if (camera.transform.position.y < 35.0f) {

				int skyNumber = Random.Range (0, 10);

				if (skyNumber == 4) {
					float randomHeight = Random.Range (projectile.transform.position.y - minHeight, projectile.transform.position.y + maxHeight);
					if (randomHeight < 1.0f) {
						randomHeight = 1.0f;
					}
					Vector3 spawnPosition = new Vector3 (spawnPoint.position.x, randomHeight, 2);
					GameObject currentSeagulls = Instantiate (seagullsPrefab, spawnPosition, Quaternion.identity);
				} else {
					int randomIndex = Random.Range (0, clouds.Length - 1);
					float randomHeight = Random.Range (projectile.transform.position.y - minHeight, projectile.transform.position.y + maxHeight);
					if (randomHeight < 1.0f) {
						randomHeight = 1.0f;
					}
					Vector3 spawnPosition = new Vector3 (spawnPoint.position.x, randomHeight, 2);
					GameObject currentCloud = Instantiate (clouds [randomIndex], spawnPosition, Quaternion.identity); 
				}
			}
			StartCoroutine (SpawnDelay ());
		}

	}

	IEnumerator SpawnDelay () 
	{
		float randomDelay = Random.Range (minDelay, maxDelay);
		yield return new WaitForSeconds (randomDelay);
		SpawnCloud ();
	}
}
