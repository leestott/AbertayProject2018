using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;
using UnityEngine.SceneManagement;

public class BreathCounting : MonoBehaviour {

	void Awake () {
        DontDestroyOnLoad(this);
	}
	
    void Start()
    {
        // Links up the breath started and breath complete functions.
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }

	// Update is called once per frame
	void Update () {
		
	}

    // Function called when breath begins.
    void OnBreathStarted(object sender)
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
            AnalyticsManager.UserBreathed();
    }

    // Function called when breath ends. Reset the fill bar.
    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        
    }
}
