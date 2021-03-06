﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fizzyo;

public class SceneChanger : MonoBehaviour
{
    // Icon and fill amount of that icon which shows key hold.
    [Header ("Image for visual indication of breath")]
    public Image holdingIcon;
    private float fillAmount;

    // Breath pressure and whether the breath has began.
    private float breathPressure = 0;
    private bool breathBegin = false;

    // How long the breath has lasted.
    private float breathTime = 0.0f;

    //Text of the minigames names to tell analytics
    [Header("The minigames names")]
    public Text minigameName;

    private void Start()
    {
        fillAmount = holdingIcon.fillAmount;

        // Links up the breath started and breath complete functions.
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }

    private void Update()
    {
        // Using the fizzyo device set the breath pressure equal to it.
        breathPressure = FizzyoFramework.Instance.Device.Pressure();

        // If the breath has began then increase the fill amount based on the pressure.
        if (breathBegin)
        {
            // Scale the breath pressure down a bit for filling icon.
            breathTime += Time.deltaTime;
            fillAmount = breathTime / 0.5f;
        }

        // Only show the icon when held for certain amount of time.
        holdingIcon.fillAmount = fillAmount;
        
        // If UI bar fills up select that minigame. Or Spacebar will select for debug mode.
        if(fillAmount >= 1)
        {
            // Using a getter to retrieve current minigame selected.
            ChangeScene(GetComponent<UISelector>().getSelected() + 1);
            AnalyticsManager.SendWhichMinigameData(minigameName.text);
            DebugManager.SendDebug("The achievement tracker should be told about the minigame now", "Achievements");
            AchievementTracker.PlayedMinigame_Ach(minigameName.text);
            DebugManager.SendDebug("This minigame has been selected " + minigameName.text, "Analytics");
        }

        // If they press escape in the main menu quit the application.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
       }

    private void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Function called when breath begins.
    void OnBreathStarted(object sender)
    {
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
