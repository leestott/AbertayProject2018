using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour {

    // Transform for the balloon prefab.
    [Header("The different level prefabs:")]
    public GameObject[] levels;

    [SerializeField]
    private int[] levelBalloonNumbers;

    [SerializeField]
    private int currentLevel = 0;

    // Start the scene with three balloons.
    void Start()
    {
        Instantiate(levels[currentLevel]);
	}

    public void BalloonPopped()
    {
        levelBalloonNumbers[currentLevel]--;
    }
	
    // TO DO: Add respawning of balloons.
    // Currently for debugging 'R' respawns the balloons.
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
	}

    public bool AllPopped()
    {
        bool ifPopped = false;

        if (levelBalloonNumbers[currentLevel] <= 0)
        {
            ifPopped = true;
        }

        return ifPopped;
    }

    public void NextLevel()
    {
        currentLevel++;

        if (currentLevel > levels.Length - 1)
        {
            currentLevel = 0;
        }

        Instantiate(levels[currentLevel]);
    }

}
