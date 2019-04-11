using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToggle : MonoBehaviour {

    [SerializeField]
    bool fizzyoToggle = false,
        breathBarToggle = false,
        analyticsToggle = false,
        achievementsToggle = false,
        scoringToggle = false,
        blowdartToggle = false;

    bool curFizzyoToggle = false,
        curBreathBarToggle = false,
        curAnalyticsToggle = false,
        curAchievementsToggle = false,
        curScoringToggle = false,
        curBlowdartToggle = false;

    void Awake ()
    {
        DontDestroyOnLoad(this);

        // Check if there is a duplicate and destroy if so.
        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(fizzyoToggle != curFizzyoToggle)
        {
            DebugManager.ToggleDebug("Fizzyo");
            curFizzyoToggle = fizzyoToggle;
        }

        if (breathBarToggle != curBreathBarToggle)
        {
            DebugManager.ToggleDebug("BreathBar");
            curBreathBarToggle = breathBarToggle;
        }

        if (analyticsToggle != curAnalyticsToggle)
        {
            DebugManager.ToggleDebug("Analytics");
            curAnalyticsToggle = analyticsToggle;
        }

        if (achievementsToggle != curAchievementsToggle)
        {
            DebugManager.ToggleDebug("Achievements");
            curAchievementsToggle = achievementsToggle;
        }

        if(scoringToggle != curScoringToggle)
        {
            DebugManager.ToggleDebug("Scoring");
            curScoringToggle = scoringToggle;
        }

        if (blowdartToggle != curBlowdartToggle)
        {
            DebugManager.ToggleDebug("Blowdart");
            curBlowdartToggle = blowdartToggle;
        }
    }
}
