using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour {

    private float timeAtStart;

	// Use this for initialization
	void Start ()
    {
        timeAtStart = Time.time;
	}

    void Update ()
    {
        // If the user presses escape go back to the main menu and set current game to hub.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
		
	}

    public void ReturnToMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Time.timeScale = 1;
        AnalyticsManager.SetCurrentGame("Hub");
        SceneManager.LoadScene("MainMenu");
    }

    // When the scene changes tell the analytics manager the time at the start of the minigame.
    void OnDestroy()
    {
        AnalyticsManager.ReportEndOfMinigame(AnalyticsManager.GetCurrentGame(), timeAtStart);
    }
}
