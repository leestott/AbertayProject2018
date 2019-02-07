using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager {
	
	public static void SendWhichMinigameData (string levelName) 
	{
		AnalyticsEvent.Custom ("Minigame_Selected", new Dictionary<string, object>
        {
            { "Minigame_Name", levelName }
        });
    }

	public static void ReportEndSession (float time, int breaths) 
	{
		Debug.Log ("SESSION_TIME: " + time);
		Debug.Log ("SESSION_BREATHS: " + breaths);

        AnalyticsEvent.Custom("Session_Details", new Dictionary<string, object>
        {
			{ "Session_Time", time },	
			{ "Session_Breaths", breaths}
		});
	}

	public static void ReportEndOfMinigame (string levelName, float time, int breaths)
	{
		Debug.Log ("LEVEL NAME: " + levelName);
		Debug.Log ("TIME: " + time);
		Debug.Log ("BREATHS: " + breaths);
		AnalyticsEvent.Custom ("Minigame_Session_Details", new Dictionary<string, object>
		{
			{ "Minigame_Name", levelName },
			{ "Minigame_Time", time },
			{ "Minigame_Breaths", breaths }
		});
	}


}
