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

    // Is the game in a state where the breath shouldn't be recorded.
    private static bool isRecordable = false;


    // Toggle for if the analytics should be sent SET THIS TO BE TRUE WHEN BUILDING.
    private static bool shouldSendAnalytics = false;


    // Getter and setter for if the breath is recordable
    public static bool GetIsRecordable() { return isRecordable; }
    public static void SetIsRecordable(bool _isRecordable) { isRecordable = _isRecordable; }

    // Getters for breaths and sets information.
    public static int GetBreathsPerSet() { return breathsPerSet; }
    public static int GetTotalSets() { return numOfSets; }

    // Getters for current breath and set information.
    public static int GetCurrBreath() { return curBreath; }
    public static int GetCurrSet() { return curSet; }

    // Getters for total number of breaths and good breaths.
    public static int GetTotalBreaths() { return totalBreaths; }
    public static int GetGoodBreaths() { return totalGoodBreaths; }

    // Getter and setter for current minigame information.
    public static void SetCurrentGame(string current) { curMgName = current; }
    public static string GetCurrentGame() { return curMgName; }



    // Initialise the set information.
    public static void SetupSets(int _breathsPerSet, int _numOfSets)
    {
        breathsPerSet = _breathsPerSet;
        numOfSets = _numOfSets;
    }

    // Function for when user breaths to keep track of all stats.
    public static void UserBreathed(bool isGoodBreath)
    {
        Debug.Log("User breathed function in analytics Manager was triggered");

        // Add to the breath information.
        totalBreaths++;
        curBreath++;
        minigameBreaths++;

        // If it is a good breath add to the various good breath stats.
        if(isGoodBreath)
        {
            minigameGoodBreaths++;
            totalGoodBreaths++;
        }

        // If the current set has finished reset current breath info.
        if(curBreath == breathsPerSet)
        {
            curSet++;
            curBreath = 0;
        }

        // Debug the information.
        Debug.Log("User breathed, total breaths now at: " + totalBreaths);
        Debug.Log("Breaths of current set: " + curBreath + "/" + breathsPerSet);
        Debug.Log("On set: " + curSet + "/" + numOfSets);
    }

    // Send which minigame has been chosen.
	public static void SendWhichMinigameData (string levelName) 
	{
        curMgName = levelName;

        if (shouldSendAnalytics)
        {
            AnalyticsEvent.Custom("Minigame_Selected", new Dictionary<string, object>
            {
                { "Minigame_Name", levelName }
            });
        }
    }

    // At the end of the minigame send analytics all the various information and reset them.
    public static void ReportEndOfMinigame(string levelName, float minigameStartTime)
    {
        // Debug the information
        Debug.Log("End of minigame report: ");
        Debug.Log("LEVEL NAME: " + levelName);
        Debug.Log("TIME_IN_MINIGAME: " + (Time.time - minigameStartTime));
        Debug.Log("BREATHS_DURING_MINIGAME: " + minigameBreaths);
        Debug.Log("GOOD_BREATHS_DURING_MINIGAME: " + minigameGoodBreaths);

        // If the time spent in the minigame is less than zero debug.
        if(0 > Time.time - minigameStartTime)
        {
            Debug.Log("ERROR TIME IN MINIGAME BELOW 0");
        }

        if (shouldSendAnalytics)
        {
            // Send the custom event.
            AnalyticsEvent.Custom("Minigame_Session_Details", new Dictionary<string, object>
            {
                { "Minigame_Name", levelName },
                { "Minigame_Time", (Time.time - minigameStartTime) },
                { "Minigame_Breaths", minigameBreaths },
                { "Minigame_Good_Breaths", minigameGoodBreaths }
            });
        }

        // Reset the minigame information.
        minigameGoodBreaths = 0;
        minigameBreaths = 0;
    }

    // Tell the analytics all of the tracked information.
    public static void ReportEndSession (float time) 
	{
        // Check the good breath achievement.
        AchievementTracker.GoodBreaths_Ach(totalGoodBreaths);

        // Debug the information.
        Debug.Log("End of session report: ");
		Debug.Log ("TOTAL_SESSION_TIME: " + time);
		Debug.Log ("TOTAL_SESSION_BREATHS: " + totalBreaths);
        Debug.Log("TOTAL_SESSION_GOOD_BREATHS: " + totalGoodBreaths);

        if (shouldSendAnalytics)
        {
            // Post information to analytics.
            AnalyticsEvent.Custom("Session_Details", new Dictionary<string, object>
            {
                { "Session_Time", time },
                { "Session_Breaths", totalBreaths},
                { "Session_Good_Breaths", totalGoodBreaths}
            });
        }
	}

}
