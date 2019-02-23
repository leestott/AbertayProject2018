using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager {

    // Overall number of breaths
    private static int totalBreaths = 0;
    private static int totalGoodBreaths = 0;

    // Current minigame name
    private static string curMgName = "Hub";

    // Set information
    private static int breathsPerSet = 0;
    private static int numOfSets = 0;

    // Current set information
    private static int curBreath = 0;
    private static int curSet = 0;

    // Current minigame information
    private static int minigameBreaths = 0;
    private static int minigameGoodBreaths = 0;


    public static void SetupSets(int _breathsPerSet, int _numOfSets)
    {
        breathsPerSet = _breathsPerSet;
        numOfSets = _numOfSets;
    }

    public static int GetBreathsPerSet()    {return breathsPerSet;}

    public static int GetTotalSets() { return numOfSets; }

    public static void UserBreathed(bool isGoodBreath)
    {
        Debug.Log("User breathed function in analytics Manager was triggered");

        totalBreaths++;
        curBreath++;
        minigameBreaths++;

        if(isGoodBreath)
        {
            minigameGoodBreaths++;
            totalGoodBreaths++;
        }

        if(curBreath == breathsPerSet)
        {
            curSet++;
            curBreath = 0;
        }

        Debug.Log("User breathed, total breaths now at: " + totalBreaths);
        Debug.Log("Breaths of current set: " + curBreath + "/" + breathsPerSet);
        Debug.Log("On set: " + curSet + "/" + numOfSets);
    }

    public static int GetTotalBreaths() {return totalBreaths;}

	public static int GetGoodBreaths () {return totalGoodBreaths;}

    public static int GetCurrBreath() { return curBreath; }

    public static int GetCurrSet() { return curSet; }

    public static void SetCurrentGame(string current)   {curMgName = current;}

    public static string GetCurrentGame()   {return curMgName;}
	
	public static void SendWhichMinigameData (string levelName) 
	{
        curMgName = levelName;

		AnalyticsEvent.Custom ("Minigame_Selected", new Dictionary<string, object>
        {
            { "Minigame_Name", levelName }
        });
    }

    public static void ReportEndOfMinigame(string levelName, float minigameStartTime)
    {
        Debug.Log("End of minigame report: ");
        Debug.Log("LEVEL NAME: " + levelName);
        Debug.Log("TIME_IN_MINIGAME: " + (Time.time - minigameStartTime));
        Debug.Log("BREATHS_DURING_MINIGAME: " + minigameBreaths);
        Debug.Log("GOOD_BREATHS_DURING_MINIGAME: " + minigameGoodBreaths);

        if(0 > Time.time - minigameStartTime)
        {
            Debug.Log("ERROR TIME IN MINIGAME BELOW 0");
        }

        AnalyticsEvent.Custom("Minigame_Session_Details", new Dictionary<string, object>
        {
            { "Minigame_Name", levelName },
            { "Minigame_Time", (Time.time - minigameStartTime) },
            { "Minigame_Breaths", minigameBreaths },
            { "Minigame_Good_Breaths", minigameGoodBreaths }
        });

        minigameGoodBreaths = 0;
        minigameBreaths = 0;
    }

    public static void ReportEndSession (float time) 
	{
        AchievementTracker.GoodBreaths_Ach(totalGoodBreaths);

        Debug.Log("End of session report: ");
		Debug.Log ("TOTAL_SESSION_TIME: " + time);
		Debug.Log ("TOTAL_SESSION_BREATHS: " + totalBreaths);
        Debug.Log("TOTAL_SESSION_GOOD_BREATHS: " + totalGoodBreaths);

        AnalyticsEvent.Custom("Session_Details", new Dictionary<string, object>
        {
			{ "Session_Time", time },	
			{ "Session_Breaths", totalBreaths},
            { "Session_Good_Breaths", totalGoodBreaths}
        });
	}

}
