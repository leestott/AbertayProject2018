using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class HelperText : MonoBehaviour {

    // The animation for the helper text.
    public Animator helpAnim;

    // The breath metre in the current scene.
    BreathMetre breathMetre;

	// Use this for initialization
	void Start ()
    {
        // Set the breaths to not be recorded when the helper text is up.
        AnalyticsManager.SetIsRecordable(false);

        breathMetre = FindObjectOfType<BreathMetre>();
    }
	
	void Update ()
    {
        // When the user presses the button move the helper text out and set it to be okay to record breaths.
        if (Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
        {
            AnalyticsManager.SetIsRecordable(true);
            helpAnim.SetTrigger("moveOut");
            breathMetre.lockBar = false;
        }

    }
}
