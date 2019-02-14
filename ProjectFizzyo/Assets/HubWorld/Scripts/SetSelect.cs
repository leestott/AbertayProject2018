using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;
using UnityEngine.SceneManagement;

public class SetSelect : MonoBehaviour {

    [SerializeField]
    private Text breathsPerSetText;
    private int breathsPerSet;

    [SerializeField]
    private Text numOfSetsText;
    private int numOfSets;

    private int currentNumber;

    // Icon and fill amount of that icon which shows key hold.
    [Header("Image for visual indication of breath")]
    public Image topHoldingIcon;
    public Image bottomHoldingIcon;
    private float fillAmount;

    // Breath pressure and whether the breath has began.
    private bool breathBegin = false;

    private float breathTime = 0.0f;

    private bool breathsConfirmed;
    private bool finishedFill;

    [SerializeField]
    private GameObject topDisplay;

    [SerializeField]
    private GameObject bottomDisplay;

    private int moveVisualsDown = -185;

    // Use this for initialization
    void Start ()
    {
        fillAmount = topHoldingIcon.fillAmount;
        fillAmount = bottomHoldingIcon.fillAmount;

        currentNumber = 8;
        numOfSets = 8;
        breathsPerSet = 8;

        breathsConfirmed = false;

        finishedFill = false;

        // Links up the breath started and breath complete functions.
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CyclingNumbers();

        LockingIn();

    }

    private void CyclingNumbers()
    {
        // On button press change minigame selected.
        if (Input.GetKeyUp(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
        {
            Debug.Log("The button was pressed.");

            currentNumber++;

            // Wrap selected around.
            if (currentNumber > 12)
            {
                currentNumber = 1;
            }

            if (!breathsConfirmed)
            {
                breathsPerSetText.text = currentNumber.ToString();
                breathsPerSet = currentNumber;
            }
            else
            {
                numOfSetsText.text = currentNumber.ToString();
                numOfSets = currentNumber;
            }
        }
    }

    private void LockingIn()
    {
        // If the breath has began then increase the fill amount based on the pressure.
        if (breathBegin && !finishedFill)
        {
            Debug.Log("Breath begin");
            // Scale the breath pressure down a bit for filling icon.
            breathTime += Time.deltaTime;
            fillAmount = breathTime / 0.5f;
        }

        // Only show the icon when held for certain amount of time.
        topHoldingIcon.fillAmount = fillAmount;
        bottomHoldingIcon.fillAmount = fillAmount;

        // If UI bar fills up select that minigame. Or Spacebar will select for debug mode.
        if (fillAmount >= 1)
        {
            finishedFill = true;
            fillAmount = 0;
            breathTime = 0;

            if (!breathsConfirmed)
            {
                breathsConfirmed = true;
                currentNumber = 8;
                topHoldingIcon.fillAmount = fillAmount;
                bottomHoldingIcon.fillAmount = fillAmount;
                topDisplay.SetActive(false);
                bottomDisplay.SetActive(true);
            }
            else
            {
                AnalyticsManager.SetupSets(breathsPerSet, numOfSets);
                SceneManager.LoadScene("MainMenu");
            }

        }
    }

    // Function called when breath begins.
    void OnBreathStarted(object sender)
    {
        finishedFill = false;
        breathTime = 0.0f;
        breathBegin = true;
    }

    // Function called when breath ends. Reset the fill bar.
    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        fillAmount = 0.0f;
        breathBegin = false;
    }
}
