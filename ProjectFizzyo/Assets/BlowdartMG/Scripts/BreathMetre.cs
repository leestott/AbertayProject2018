using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class BreathMetre : MonoBehaviour {

    public Scrollbar breathMetre;

    private float breathPressure;
    private float fillAmount;

    public bool reset = false;
	
	void Update ()
    {
        breathPressure = FizzyoFramework.Instance.Device.Pressure();
        if (fillAmount < 100f)
        {
            fillAmount += breathPressure;
        }

        breathMetre.size = fillAmount;

        if(reset)
        {
            fillAmount = 0.0f;
        }
    }
}
