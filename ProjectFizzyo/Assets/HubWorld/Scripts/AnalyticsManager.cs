using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager {

    private static int numOfBreaths = 0;
    private static string curMgName = "";

    public static void UserBreathed()
    {
        numOfBreaths++;
        Debug.Log("User breathed, breaths now at: " + numOfBreaths);
    }

    public static int GetTotalBreaths()
    {
        return numOfBreaths;
    }

    public static void SetCurrentGame(string current)
    {
        curMgName = current;
    }

    public static string GetCurrentGame()
    {
        return curMgName;
    }
	
	public static void SendWhichMinigameData (string levelName) 
	{
        curMgName = levelName;

		AnalyticsEvent.Custom ("Minigame_Selected", new Dictionary<string, object>
        {
            { "Minigame_Name", levelName }
        });
    }

    public static void ReportEndOfMinigame(string levelName, float time, int breaths)
    {
        Debug.Log("End of minigame report: ");
        Debug.Log("LEVEL NAME: " + levelName);
        Debug.Log("TIME_IN_MINIGAME: " + time);
        Debug.Log("BREATHS_DURING_MINIGAME: " + breaths);

        AnalyticsEvent.Custom("Minigame_Session_Details", new Dictionary<string, object>
        {
            { "Minigame_Name", levelName },
            { "Minigame_Time", time },
            { "Minigame_Breaths", breaths }
        });
    }

    public static void ReportEndSession (float time) 
	{
        Debug.Log("End of session report: ");
		Debug.Log ("TOTAL_SESSION_TIME: " + time);
		Debug.Log ("TOTAL_SESSION_BREATHS: " + numOfBreaths);

        AnalyticsEvent.Custom("Session_Details", new Dictionary<string, object>
        {
			{ "Session_Time", time },	
			{ "Session_Breaths", numOfBreaths}
		});
	}

}
