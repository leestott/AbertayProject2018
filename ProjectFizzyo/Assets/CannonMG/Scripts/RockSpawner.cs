using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {

	public GameObject[] rockPrefabs;

	public Transform rockSpawnPoint;

	bool hasStarted = false;

	public float minDelay;
	public float maxDelay;

	void Update () 
	{
		if (GameObject.FindGameObjectWithTag("CharacterProjectile") != null && !hasStarted)
		{
			hasStarted = true;
			StartCoroutine (SpawnTimer ());
		}
	}


	void SpawnRock () 
	{
		int rockNumber = Random.Range (0, rockPrefabs.Length);
		float randomYPos = Random.Range (-4f, -1f);
		Vector3 rockSpawnPosition = new Vector3 (rockSpawnPoint.position.x, randomYPos, -8.0f);

		GameObject currentRock = Instantiate (rockPrefabs [rockNumber], rockSpawnPosition, Quaternion.identity);

		if (GameObject.FindGameObjectWithTag ("CharacterProjectile") != null)
		{
			StartCoroutine (SpawnTimer ());
		}
	}

	IEnumerator SpawnTimer ()
	{
		float delay = Random.Range (minDelay, maxDelay);

		yield return new WaitForSeconds (delay);

		SpawnRock ();
	}
}
