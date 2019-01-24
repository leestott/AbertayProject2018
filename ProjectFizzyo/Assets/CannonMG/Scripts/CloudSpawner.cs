using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	Transform cloudSpawnPoint;
	Transform cloudDeletePoint;

	public GameObject cloudPrefab;

	public float minDelay;
	public float maxDelay;

	public float minSize;
	public float maxSize;

	public float minHeight;
	public float maxHeight;

	bool hasStarted = false;

	void Start () 
	{
		cloudSpawnPoint = GameObject.Find ("CloudSpawnPoint").transform;
		cloudDeletePoint = GameObject.Find ("CloudDeletePoint").transform;
	}

	void Update () 
	{
		if (GameObject.FindGameObjectWithTag("CharacterProjectile") != null && !hasStarted)
		{
			hasStarted = true;
			StartCoroutine (SpawnTimer ());
		}
	}

	void SpawnCloud () 
	{
		float spawnHeight = Random.Range (minHeight, maxHeight);
		float spawnSize = Random.Range (minSize, maxSize);

		Vector3 spawnPosition = new Vector3 (cloudSpawnPoint.position.x, spawnHeight, 1);

		GameObject cloudInstance = Instantiate (cloudPrefab, spawnPosition, Quaternion.identity) as GameObject;
		cloudInstance.transform.localScale = new Vector3 (spawnSize, spawnSize, spawnSize);

		if (GameObject.FindGameObjectWithTag ("CharacterProjectile") != null)
		{
			StartCoroutine (SpawnTimer ());
		}
	}

	IEnumerator SpawnTimer ()
	{
		float delay = Random.Range (minDelay, maxDelay);

		yield return new WaitForSeconds (delay);

		SpawnCloud ();
	}
}
