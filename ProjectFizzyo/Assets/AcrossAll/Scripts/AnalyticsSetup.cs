using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsSetup : MonoBehaviour {

    [SerializeField]
    private AnalyticsConfig config;

    private void Awake()
    {
        AnalyticsManager.SetConfig(config);
    }
}
