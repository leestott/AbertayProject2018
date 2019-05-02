using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour {

    // Transform for the balloon prefab.
    [Header("The different level prefabs:")]
    public GameObject[] levels;

    // How many balloons are in each level.
    [SerializeField]
    private int[] levelBalloonNumbers;

    // Which level is currently on.
    [SerializeField]
    private int currentLevel = 0;

    // Start the scene with three balloons.
    void Start()
    {
        Instantiate(levels[currentLevel]);
	}

    // For each balloon popped subtract from how many balloons are in the level.
    public void BalloonPopped()
    {
        levelBalloonNumbers[currentLevel]--;
    }
	
    // TO DO: Remove Debug for changing level.
    // Currently for debugging 'N' goes to the next level.
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }

        // If all the balloons are popped move on to the next level.
        if (AllPopped())
        {
            NextLevel();
        }
    }

    // Checks if the current level has run out of balloons if so return true.
    public bool AllPopped()
    {
        bool ifPopped = false;

        if (levelBalloonNumbers[currentLevel] <= 0)
        {
            ifPopped = true;
        }

        return ifPopped;
    }

    // Change the level if at the end wrap around.
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
