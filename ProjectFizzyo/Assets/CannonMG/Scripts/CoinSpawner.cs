using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	public GameObject coinPrefab;

	public float minDelay = 4.0f;
	public float maxDelay = 8.0f;

	float spacing = 0.6f;
	bool hasStarted = false;

	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("CharacterProjectile") != null && !hasStarted)
		{
			hasStarted = true;
			StartCoroutine (SpawnDelay ());
		} 
		else if (GameObject.FindGameObjectWithTag ("CharacterProjectile") == null)
		{
			hasStarted = false;
		}
	}

	void SpawnSquare () 
	{
		int randomX = Random.Range (2, 6);
		int randomY = Random.Range (2, 6);

		for (int y = 0; y < randomY; y++) 
		{
			for (int x = 0; x < randomX; x++) 
			{
				Vector3 spawnPos = new Vector3 (transform.position.x + (x * spacing), transform.position.y + (y * spacing), 0);
				GameObject currentCoin = Instantiate (coinPrefab, spawnPos, Quaternion.identity) as GameObject;
			}
		}

		if (GameObject.FindGameObjectWithTag ("CharacterProjectile") != null) 
		{
			StartCoroutine (SpawnDelay ());
		}
	}

	IEnumerator SpawnDelay() 
	{
		float randomDelay = Random.Range (minDelay, maxDelay);
		yield return new WaitForSeconds (randomDelay);
		SpawnSquare ();
		Debug.Log ("Spawn");
	}
}
