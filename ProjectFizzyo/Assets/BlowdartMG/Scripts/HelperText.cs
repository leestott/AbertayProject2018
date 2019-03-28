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
        breathMetre = FindObjectOfType<BreathMetre>();
    }
	
	void Update ()
    {
        // When the user presses the button move the helper text out.
        if (Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
        {
            helpAnim.SetTrigger("moveOut");
            breathMetre.lockBar = false;
        }

    }
}
