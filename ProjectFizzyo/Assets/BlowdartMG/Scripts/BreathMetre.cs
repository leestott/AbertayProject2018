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

    public bool lockBar = true;
    public bool beganBreath;
    public float breathTime = 0.0f;

    public Animator barFull;

    // Reset the bar to 0.
    public bool reset = false;

    public void Start()
    {
        fillAmount = 0.0f;
        breathTime = 0.0f;
        // Links up the breath started and breath complete functions.
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }

    void Update ()
    {

        if (!lockBar)
        {
            if (beganBreath)
            {
                breathTime += Time.deltaTime;
                fillAmount = breathTime / 3;
            }

            // Links the fill amount float to the breathMetre.
            breathMetre.size = fillAmount;
        }

        if(fillAmount>=1)
        {
            barFull.SetBool("barFull", true);
        }

        // Reset the fill amount.
        if(reset)
        {
            fillAmount = 0.0f;
            breathTime = 0.0f;
            reset = false;
            lockBar = false;
        }
    }

    private void OnBreathStarted(object sender)
    {
        Debug.Log("Began breath");
        breathTime = 0.0f;
        beganBreath = true;
    }

    private void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        beganBreath = false;
        lockBar = true;
    }
}
