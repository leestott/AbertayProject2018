using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fizzyo;

public class SceneChanger : MonoBehaviour
{
    // Icon and fill amount of that icon which shows key hold.
    public Image holdingIcon;
    private float fillAmount;

    // Breath pressure and whether the breath has began.
    private float breathPressure = 0;
    private bool breathBegin = false;

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
            fillAmount += breathPressure/25;
        }

        // Only show the icon when held for certain amount of time.
        holdingIcon.fillAmount = fillAmount;
        
        // If UI bar fills up select that minigame. Or Spacebar will select for debug mode.
        if(fillAmount >= 1)
        {
            // TO DO: Remove debug code of automatically going to blowdart game
            // Using a getter to retrieve current minigame selected.
            //sceneChanger(GetComponent<UISelector>().getSelected());
            sceneChanger(2);
        }
    }

    private void sceneChanger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Function called when breath begins.
    void OnBreathStarted(object sender)
    {
        breathBegin = true;
    }

    // Function called when breath ends. Reset the fill bar.
    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        fillAmount = 0.0f;
        breathBegin = false;
    }
}
