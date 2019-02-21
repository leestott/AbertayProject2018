﻿using System.Collections;
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

    public bool lockBar = true;
    public bool beganBreath;
    public float breathTime = 0.0f;

	public int goodBreaths = 0;
	public int totalBreaths = 0;

    public int scoreMultiplier = 1;

    public Animator barFull;

    // Set to the maximum calibrated breath length
    private float maxCaliBreath = 0.0f;

    // Reset the bar to 0.
    public bool reset = false;

    // For screenshake
    ScreenShake screenShake;

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
            barFull.SetBool("barFull", false);
        }
    }

    private void OnBreathStarted(object sender)
    {
        //Debug.Log("Began breath");
        breathTime = 0.0f;
        beganBreath = true;
    }

    private void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        beganBreath = false;
        lockBar = true;

        if (AnalyticsManager.GetCurrentGame() != "Hub")
        {
            if (fillAmount >= 0.1f)
            {
                Debug.Log("IS BREATH FULL: " + e.IsBreathFull);
                if (e.IsBreathFull && fillAmount >= 0.7f)
                {
                    AnalyticsManager.UserBreathed(true);
                    scoreMultiplier *= 2;
                    if (scoreMultiplier > 16)
                    {
                        scoreMultiplier = 16;
                    }
                }
                else
                {
                    AnalyticsManager.UserBreathed(false);
                    scoreMultiplier = 1;
                }
            }
        }

        Debug.Log("score multi: " + scoreMultiplier);
    }
}
