using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadDisplay : MonoBehaviour {

	public float displayTime;

	Animator animator;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		StartCoroutine (displayDelay ());
	}

	IEnumerator displayDelay ()
	{
		yield return new WaitForSeconds (displayTime);
		animator.SetBool ("shouldShow", true);
	}
}
