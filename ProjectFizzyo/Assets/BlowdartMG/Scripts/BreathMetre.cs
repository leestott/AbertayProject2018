using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class BreathMetre : MonoBehaviour {

    // The corresponding visual element to the breath pressure.
    public Scrollbar breathMetre;

    // How far the bar is currently filled.
    [Range (0, 1)]
    public float fillAmount = 0;

    // Prevent the bar from being filled.
    public bool lockBar = true;

    // Has began breathing and the time spent breathing.
    public bool beganBreath;
    public float breathTime = 0.0f;

    // Total number of total and good breaths.
	public int goodBreaths = 0;
	public int totalBreaths = 0;

    // Multiplier for score which increases with good breaths.
    public int scoreMultiplier = 1;

    // The bars animator for when it is full.
    public Animator barFull;

    // Set to the maximum calibrated breath length.
    private float maxCaliBreath = 0.0f;

    // Reset the bar to 0.
    public bool reset = false;

    // For screenshake.
    ScreenShake screenShake;

    // Initialise the variables.
    public void Start()
    {
        fillAmount = 0.0f;
        breathTime = 0.0f;

        // Links up the breath started and breath complete functions.
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;

        maxCaliBreath = FizzyoFramework.Instance.Device.maxBreathCalibrated;
        Debug.Log("This is the maximum calibrated breath " + maxCaliBreath);

        screenShake = FindObjectOfType<ScreenShake>();
    }

    void Update ()
    {
        // If bar is not locked increase fill amount based on the breaths length vs calibrated breath.
        if (!lockBar)
        {
            if (beganBreath)
            {
                breathTime += Time.deltaTime;
                fillAmount = breathTime / maxCaliBreath;
            }

            // Links the fill amount float to the breathMetre.
            breathMetre.size = fillAmount;
        }
        else
        {
            AnalyticsManager.SetIsRecordable(false);
        }

        // If the bar is full lock the bar and shake the screen.
        if ((fillAmount >= 1) && (!lockBar) && (!reset))
        {
            barFull.SetBool("barFull", true);
            screenShake.ShakeScreen();
            lockBar = true;
        }

        // Reset the fill amount.
        if(reset)
        {
            fillAmount = 0.0f;
            breathTime = 0.0f;
            reset = false;
            lockBar = false;
            AnalyticsManager.SetIsRecordable(true);
            barFull.SetBool("barFull", false);
        }
    }

    // When the user starts a breath reset breathtime and set began breath to true.
    private void OnBreathStarted(object sender)
    {
        breathTime = 0.0f;
        beganBreath = true;
    }

    // When the breath ends lock the bar and deal with analytics.
    private void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        beganBreath = false;
        lockBar = true;

        // Check if its in the hub.
        if (AnalyticsManager.GetIsRecordable() == true)
        {
            // Ensure breath wasn't accidental
            if (fillAmount >= 0.1f)
            {
                bool wasGoodBreath;

                // If the breath is a good breath.
                if (e.IsBreathFull && fillAmount >= 0.7f)
                {
                    // Set good breath to true.
                    wasGoodBreath = true;

                    // Double the score multiplier until its at 16.
                    scoreMultiplier *= 2;
                    if (scoreMultiplier > 16)
                    {
                        scoreMultiplier = 16;
                    }
                }
                else
                {
                    // Set the good breath to false and reset the score multiplier.
                    wasGoodBreath = false;
                    scoreMultiplier = 1;
                }

                // Tell the analytics manager they breathed and if it was of good quality.
                AnalyticsManager.UserBreathed(wasGoodBreath);
            }
        }
    }
}
