using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class SetDisplayInfo : MonoBehaviour {

    public Text breathsDataText;
    public Text setsDataText;

    private int breathMax;
    private int setMax;

    public GameObject popupPrefab;
    private GameObject currPopup;
    private bool isPopupDisplayed;
    private int currSet;

    public bool GetIsPopupDisplayed()
    {
        return isPopupDisplayed;
    }

	// Use this for initialization
	void Start ()
    {
        breathMax = AnalyticsManager.GetBreathsPerSet();
        setMax = AnalyticsManager.GetTotalSets();
        currSet = AnalyticsManager.GetCurrSet();
	}
	
	// Update is called once per frame
	void Update ()
    {
		breathsDataText.text = "Breaths "+ AnalyticsManager.GetCurrBreath() + "/" + breathMax;
        setsDataText.text = "Sets " + AnalyticsManager.GetCurrSet() + "/" + setMax;

        if(currSet != AnalyticsManager.GetCurrSet())
        {
            currSet = AnalyticsManager.GetCurrSet();
            isPopupDisplayed = true;
            Popup();
        }

        if (isPopupDisplayed)
        {
            if (Input.GetKeyUp(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
            {
                Destroy(currPopup);
                Time.timeScale = 1;
                isPopupDisplayed = false;
            }
        }

    }

    private void Popup()
    {
        Debug.Log("Finished a set, instatiating prefab");
        currPopup = Instantiate(popupPrefab);
        currPopup.transform.SetParent(GameObject.Find("UICanvas").transform);
        currPopup.transform.localPosition = new Vector3(0.0f, 0.0f);
        Time.timeScale = 0;
    }
}
