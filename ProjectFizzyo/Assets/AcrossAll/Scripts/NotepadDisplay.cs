using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadDisplay : MonoBehaviour
{
    // Time for how long it is shown before dissappearing.
	public float displayTime;

    // The animator.
	Animator animator;

    // Initialise the animator and start counting the time.
	void Start () 
	{
		animator = GetComponent<Animator> ();
		StartCoroutine (DisplayDelay ());
	}

    // After displayTime has passed then hide the panel.
	IEnumerator DisplayDelay ()
	{
		yield return new WaitForSeconds (displayTime);
		animator.SetBool ("shouldShow", true);
	}
}
