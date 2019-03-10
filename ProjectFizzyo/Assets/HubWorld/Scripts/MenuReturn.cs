using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour {

    private float timeAtStart;

	// Use this for initialization
	void Start () {
        timeAtStart = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            AnalyticsManager.SetCurrentGame("Hub");
            SceneManager.LoadScene("MainMenu");
        }
		
	}

    void OnDestroy()
    {
        AnalyticsManager.ReportEndOfMinigame(AnalyticsManager.GetCurrentGame(), timeAtStart);
    }
}
