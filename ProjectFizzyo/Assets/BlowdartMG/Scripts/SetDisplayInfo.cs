using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class SetDisplayInfo : MonoBehaviour {

    // The Text UI.
    public Text breathsDataText;
    public Text setsDataText;

    // The values for breath and sets total.
    private int breathMax;
    private int setMax;

    // Popup prefab and the current popup.
    public GameObject popupPrefab;
    private GameObject currPopup;
    private bool isPopupDisplayed;

    // The set currently on.
    private int currSet;

    // Check if popup is displayed.
    public bool GetIsPopupDisplayed()
    {
        return isPopupDisplayed;
    }

	// Get the information from analytics about the breaths/sets.
	void Start ()
    {
        breathMax = AnalyticsManager.GetBreathsPerSet();
        setMax = AnalyticsManager.GetTotalSets();
        currSet = AnalyticsManager.GetCurrSet();
	}
	
	void Update ()
    {
        // Breaths UI updated from analytics.
		breathsDataText.text = "Breaths "+ AnalyticsManager.GetCurrBreath() + "/" + breathMax;
        setsDataText.text = "Sets " + AnalyticsManager.GetCurrSet() + "/" + setMax;

        // If current set is finished display popup.
        if(currSet != AnalyticsManager.GetCurrSet())
        {
            currSet = AnalyticsManager.GetCurrSet();
            isPopupDisplayed = true;
            Popup();
        }

        // If the popup is displayed destroy the popup when the button is pressed.
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

    // Instantiate a popup and set it to the UI canvas.
    private void Popup()
    {
        Debug.Log("Finished a set, instatiating prefab");
        currPopup = Instantiate(popupPrefab);
        currPopup.transform.SetParent(GameObject.Find("UICanvas").transform);
        currPopup.transform.localPosition = new Vector3(0.0f, 0.0f);
        Time.timeScale = 0;
    }
}
