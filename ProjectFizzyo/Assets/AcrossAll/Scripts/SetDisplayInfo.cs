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
    private bool isPopupDisplayed = false;

    // The set currently on.
    private int currSet;

    // The countdown bar.
    public Image countdown;

    // Timer for how long huff cough pop is displayed.
    public float displayLength = 1.0f;
    public float timer = 0.0f;

    // Reference to the breath metre
    private BreathMetre breathMetre;

    // Getter for if popup is displayed.
    public bool GetIsPopupDisplayed() { return isPopupDisplayed;}

	// Get the information from analytics about the breaths/sets and initialise the breath metre.
	void Start ()
    {
        breathMetre = FindObjectOfType<BreathMetre>();

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
            if (currSet < AnalyticsManager.GetTotalSets())
            {
                isPopupDisplayed = true;
                timer = 0.0f;
                Popup();
            }
        }
        
        // Add to the timer with unscaled delta time as that is unaffected by timescale.
        timer += Time.unscaledDeltaTime;

        // If the popup is displayed destroy the popup when timer elapses.
        if (isPopupDisplayed)
        {
            // Dont record breaths during popup and lock the bar.
            AnalyticsManager.SetIsRecordable(false);
            breathMetre.lockBar = true;

            // Get the countdown bar image.
            countdown = currPopup.transform.GetChild(1).GetComponent<Image>();

            // Lerp the fill amount towards 0 over 10 seconds.
            countdown.fillAmount = Mathf.Lerp(1.0f, 0.0f, timer/ displayLength);

            // If the display length has been reached then tell analytics to record breaths again,
            // reset the timer, destroy the popup and reset the timescale.
            if (timer > displayLength)
            {
                AnalyticsManager.SetIsRecordable(true);
                breathMetre.lockBar = false;
                timer = 0.0f;
                Destroy(currPopup);
                Time.timeScale = 1;
                isPopupDisplayed = false;
            }
        }

    }

    // Instantiate a popup and set it to the UI canvas.
    private void Popup()
    {
        DebugManager.SendDebug("Finished a set, instatiating prefab", "BreathBar");
        currPopup = Instantiate(popupPrefab);
        currPopup.transform.SetParent(GameObject.Find("UICanvas").transform);
        currPopup.transform.localPosition = new Vector3(0.0f, 0.0f);
        Time.timeScale = 0;
    }
}
