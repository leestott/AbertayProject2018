using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject[] obstaclePrefabs;
	public GameObject[] positiveObstacles;
	public GameObject[] negativeObstacles;
	public Transform obstacleSpawnPoint;

	public float minDelayTime;
	public float maxDelayTime;

	public float multiplier;

	public Rigidbody2D projectileRB;

	public float playerSpeed;
	public float publicDelayTime;

	public GameObject projectile;

	public bool hasFound = false;
	public bool hasRB = false;

	public bool hasSpawned = false;

	[Range (0, 1)]
	public float positiveObstacleBias;

	void Start () 
	{
		StartCoroutine (SpawnDelay ());
	}

	void Update () 
	{
		if (!hasFound)
		{
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
		}
		if (!hasRB && projectile != null) 
		{
			projectileRB = projectile.GetComponent<Rigidbody2D> ();
		}

		if (projectileRB != null)
		{
			hasRB = true;
		}
		else
		{
			hasRB = false;
		}

		if (projectile != null) 
		{
			hasFound = true;
		} 
		else 
		{
			hasFound = false;
			projectileRB = null;
			hasRB = false;
			hasSpawned = false;
		}
	}

	void SpawnObstacle () 
	{
		int randomBias = Random.Range (0, 100);
		int currentPositiveBias = (int)(positiveObstacleBias * 100);

		if (randomBias <= currentPositiveBias)
		{
			int randomObstacleIndex = Random.Range (0, positiveObstacles.Length);
			Vector3 spawnPosition = new Vector3 (obstacleSpawnPoint.position.x, positiveObstacles [randomObstacleIndex].transform.position.y, 0);
			GameObject currentObstacle = Instantiate (positiveObstacles [randomObstacleIndex], spawnPosition, positiveObstacles[randomObstacleIndex].transform.rotation) as GameObject;
		} 
		else 
		{
			int randomObstacleIndex = Random.Range (0, negativeObstacles.Length);
			Vector3 spawnPosition = new Vector3 (obstacleSpawnPoint.position.x, negativeObstacles [randomObstacleIndex].transform.position.y, 0);
			GameObject currentObstacle = Instantiate (negativeObstacles [randomObstacleIndex], spawnPosition, Quaternion.identity) as GameObject;
		} 
		StartCoroutine (SpawnDelay ());
	}

	IEnumerator SpawnDelay () 
	{
		float delayTime = Random.Range (minDelayTime, maxDelayTime);
		if (projectileRB != null)
		{
			if (projectileRB.velocity.x > 0)
			{
				delayTime /= projectileRB.velocity.x * multiplier;
			}
		}
		publicDelayTime = delayTime;
		//Debug.Log ("OBSTACLE DELAY TIME: " + delayTime);
		yield return new WaitForSeconds (delayTime);
			
		if (hasFound && projectile != null) 
		{
			SpawnObstacle ();
		}
		else 
		{
			StartCoroutine (SpawnDelay ());
		}
	}
}
