using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager {
	
	public static void ReportTestData (float number, string levelname) 
	{
		AnalyticsEvent.Custom ("test_data", new Dictionary<string, object> 
		{
				{ "test_number", number},
				{ "level_name", levelname}
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
