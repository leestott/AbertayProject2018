using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugManager 
{
    static bool fizzyoToggle = false,
        breathBarToggle = false,
        analyticsToggle = false,
        achievementsToggle = false,
        scoringToggle = false,
        blowdartToggle = false;

    public static void ToggleDebug(string type)
    {
        switch (type)
        {
            case "Fizzyo":
                {
                    fizzyoToggle = !fizzyoToggle;
                    break;
                }
            case "BreathBar":
                {
                    breathBarToggle = !breathBarToggle;
                    break;
                }
            case "Analytics":
                {
                    analyticsToggle = !analyticsToggle;
                    break;
                }
            case "Achievements":
                {
                    achievementsToggle = !achievementsToggle;
                    break;
                }
            case "Scoring":
                {
                    scoringToggle = !scoringToggle;
                    break;
                }
            case "Blowdart":
                {
                    blowdartToggle = !blowdartToggle;
                    break;
                }
        }
    }

    public static void SendDebug(string debugThis, string type)
    {
        switch (type)
        {
            case "Fizzyo":
                {
                    if(fizzyoToggle)
                    {
                        Debug.Log(debugThis);
                    }
                    break;
                }
            case "BreathBar":
                {
                    if(breathBarToggle)
                    {
                        Debug.Log(debugThis);
                    }
                    break;
                }
            case "Analytics":
                {
                    if (analyticsToggle)
                    {
                        Debug.Log(debugThis);
                    }
                    break; 
                }
            case "Achievements":
                {
                    if (achievementsToggle)
                    {
                        Debug.Log(debugThis);
                    }
                    break; 
                }
            case "Scoring":
                {
                    if (scoringToggle)
                    {
                        Debug.Log(debugThis);
                    }
                    break;
                }
            case "Blowdart":
                {
                    if (blowdartToggle)
                    {
                        Debug.Log(debugThis);
                    }
                    break; 
                }
        }
        
    }
}
