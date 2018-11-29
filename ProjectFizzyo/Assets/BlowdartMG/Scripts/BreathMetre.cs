using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class BreathMetre : MonoBehaviour {

    // The corresponding visual element to the breath pressure.
    public Scrollbar breathMetre;

    // How far the bar is currently filled.
    private float fillAmount;

    // Links the bars power to the dart power.
    public bool reset = false;
	
	void Update ()
    {
        // Connect the fill amount to the pressure.
        fillAmount += FizzyoFramework.Instance.Device.Pressure()/100;

        // Links the fill amount float to the breathMetre.
        breathMetre.size = fillAmount;

        // Reset the fill amount.
        if(reset)
        {
            fillAmount = 0.0f;
            reset = false;
        }
    }
}
