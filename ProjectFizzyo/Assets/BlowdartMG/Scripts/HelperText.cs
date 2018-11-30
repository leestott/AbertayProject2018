using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class HelperText : MonoBehaviour {

    public Animator helpAnim;
    BreathMetre breathMetre;

	// Use this for initialization
	void Start () {
        breathMetre = FindObjectOfType<BreathMetre>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
        {
            helpAnim.SetTrigger("moveOut");
            breathMetre.lockBar = false;
        }

    }
}
