using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;
using UnityEngine.SceneManagement;

public class SetSelect : MonoBehaviour
{
    // Info + text for the breath per set.
    [SerializeField]
    private Text breathsPerSetText;
    private int breathsPerSet;

    // Info + text for the num of set.
    [SerializeField]
    private Text numOfSetsText;
    private int numOfSets;

    // What number they are on.
    private int currentNumber;

    // The min and max the numbers can go to.
	private int minNumber = 3;
    private int maxNumber = 12;

    // Icon and fill amount of that icon which shows key hold.
    [Header("Image for visual indication of breath")]
    public Image topHoldingIcon;
    public Image bottomHoldingIcon;
    private float fillAmount;

    // Breath pressure and whether the breath has began.
    private bool breathBegin = false;

    // How long the breath has lasted.
    private float breathTime = 0.0f;

    // Check if the breaths per set has been confirmed and have they selected the option.
    private bool breathsConfirmed;
    private bool finishedFill;

    [SerializeField]
    private GameObject topDisplay;

    [SerializeField]
    private GameObject bottomDisplay;

    private int moveVisualsDown = -185;

    // Initalise the variables
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
	
	void Update ()
    {
        // Deals with changing the values.
        CyclingNumbers();

        // Deals with locking in the selection.
        LockingIn();
    }

    private void CyclingNumbers()
    {
        // On button press increase current number.
        if (Input.GetKeyUp(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
        {
            //Debug.Log("The button was pressed.");

            currentNumber++;

            // Wrap selected around.
			if (currentNumber > maxNumber)
            {
				currentNumber = minNumber;
            }

            // If breaths per set hasnt been confirmed update that number else update the set number.
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
        // If the breath has began then increase the fill amount based on the time.
        if (breathBegin && !finishedFill)
        {
            //Debug.Log("Breath begin");
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

            // If the breaths per set hasn't been confirmed then lock it in and set up UI for set selection
            // Else tell analytics the set information and load the main menu.
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
