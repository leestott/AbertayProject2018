using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDisplayInfo : MonoBehaviour {

    public Text breathsDataText;
    public Text setsDataText;

    private int breathMax;
    private int setMax;

	// Use this for initialization
	void Start ()
    {
        breathMax = AnalyticsManager.GetBreathsPerSet();
        setMax = AnalyticsManager.GetTotalSets();
	}
	
	// Update is called once per frame
	void Update ()
    {
		breathsDataText.text = "Breaths "+ AnalyticsManager.GetCurrBreath() + "/" + breathMax;
        setsDataText.text = "Sets " + AnalyticsManager.GetCurrSet() + "/" + setMax;
    }
}
