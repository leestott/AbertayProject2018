using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AnalyticsConfig", menuName = "Analytics/Config", order = 1)]
public class AnalyticsConfig : ScriptableObject
{
    public bool sendAnalytics = false;
}