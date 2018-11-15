using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour {

    // Transform for the balloon prefab.
    [Header("The balloon prefab:")]
    public Transform balloon;

    // Start the scene with three balloons.
    void Start()
    {
        spawnMore();
	}
	
    // TO DO: Add respawning of balloons.
    // Currently for debugging 'R' respawns the balloons.
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R))
        {
            spawnMore();
        }
	}

    // Instantiates three balloons evenly spaced out.
    void spawnMore()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(balloon, new Vector3(i * 3.0f, 0, 0), Quaternion.identity);
        }
    }
}
