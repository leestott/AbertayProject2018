using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager {

    // Overall number of breaths
    private static int totalBreaths = 0;

    // Current minigame name
    private static string curMgName = "";

    // Set information
    private static int breathsPerSet = 0;
    private static int numOfSets = 0;

    // Current set information
    private static int curBreath = 0;
    private static int curSet = 0;

    // Current minigame information
    private static int minigameBreaths = 0;

    public static void SetupSets(int _breathsPerSet, int _numOfSets)
    {
        breathsPerSet = _breathsPerSet;
        numOfSets = _numOfSets;
    }

    public static int getBreathsPerSet()    {return breathsPerSet;}

    public static void UserBreathed()
    {
        totalBreaths++;
        curBreath++;
        minigameBreaths++;

        if(curBreath > breathsPerSet)
        {
            curSet++;
            curBreath = 0;
        }

        Debug.Log("User breathed, total breaths now at: " + totalBreaths);
        Debug.Log("Breaths of current set: " + curBreath + "/" + breathsPerSet);
        Debug.Log("On set: " + curSet + "/" + numOfSets);
    }

    public static int GetTotalBreaths() {return totalBreaths; }

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

        AnalyticsEvent.Custom("Minigame_Session_Details", new Dictionary<string, object>
        {
            { "Minigame_Name", levelName },
            { "Minigame_Time", (Time.time - minigameStartTime) },
            { "Minigame_Breaths", minigameBreaths }
        });

        minigameBreaths = 0;
    }

    public static void ReportEndSession (float time) 
	{
        Debug.Log("End of session report: ");
		Debug.Log ("TOTAL_SESSION_TIME: " + time);
		Debug.Log ("TOTAL_SESSION_BREATHS: " + totalBreaths);

        AnalyticsEvent.Custom("Session_Details", new Dictionary<string, object>
        {
			{ "Session_Time", time },	
			{ "Session_Breaths", totalBreaths}
		});
	}

}
