using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootageScript : MonoBehaviour {

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			Time.timeScale = 0.25f;
		}
		if (Input.GetKeyDown (KeyCode.G)) 
		{
			Time.timeScale = 0.5f;
		}
		if (Input.GetKeyDown (KeyCode.H)) 
		{
			Time.timeScale = 1.0f;
		}
	}
}
