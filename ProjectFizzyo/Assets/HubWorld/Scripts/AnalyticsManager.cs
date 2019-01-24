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
		AnalyticsEvent.Custom ("Session_Details", new Dictionary<string, object> 
		{
			{ "Session_Time", time },	
			{ "Session_Breaths", breaths}
		});
	}
}
