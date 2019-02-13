using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour {

    // Transform for the balloon prefab.
    [Header("The different level prefabs:")]
    public GameObject[] levels;

    [SerializeField]
    private int currentLevel = 0;

    // Start the scene with three balloons.
    void Start()
    {
        Instantiate(levels[currentLevel]);
	}
	
    // TO DO: Add respawning of balloons.
    // Currently for debugging 'R' respawns the balloons.
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.N))
        {
            currentLevel++;

            if(currentLevel > levels.Length-1)
            {
                currentLevel = 0;
            }

            Instantiate(levels[currentLevel]);
        }
	}

}
