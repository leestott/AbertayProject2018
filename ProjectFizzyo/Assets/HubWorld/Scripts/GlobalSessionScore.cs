using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class GlobalSessionScore : MonoBehaviour
{
    // The reference to the end session popup.
	public GameObject endSessionWindowPrefab;
	GameObject currentPanel;

    // Reference to the canvas to apply the UI prefab to.
	GameObject currentCanvas;

    // The score based on breath quality.
	public int qualityScore = 0;

    // The number of total breaths required based on entered sets and breath per set.
	private int requiredBreaths;

    // Is the session over and is the box displayed.
	bool sessionOver = false;
	public bool boxDisplayed = false;

    // The current minigames scoring system.
    private MinigameScoring minigameScoring;

    // The stars that fill up based on score.
    GameObject starFill;
	Image starImage;

    // The sound affect associated with finishing.
    AudioSource audioSource;
    public AudioClip achievementSound;
    private bool audioPlayed = false;

    // Initialise the required breath and check if the score manager has already been created.
    void Start () 
	{
        // Dont destroy on scene change.
		DontDestroyOnLoad (this);

        // Destroy this if it has already been created.
        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(this);
        }
        
        // Calculate the number of required breaths.
        requiredBreaths = AnalyticsManager.GetBreathsPerSet () * AnalyticsManager.GetTotalSets ();

        // Debug the information.
		Debug.Log ("User Sets: " + AnalyticsManager.GetTotalSets ());
		Debug.Log ("User Breaths per Set: " + AnalyticsManager.GetBreathsPerSet ());
		Debug.Log ("Required Breaths: " + requiredBreaths);
	}

	void Update () 
	{
        // Check if the session has finished by checking required breaths.
        sessionOver = IsSessionFinished();

        // If the box is displayed play the audio and fill the stars based on the score.
		if (boxDisplayed) 
		{
            // Check if the audio has already been played.
            if(!audioPlayed)
            {
                audioSource = FindObjectOfType<AudioSource>();
                audioSource.PlayOneShot(achievementSound);
                audioPlayed = true;
            }

            // Fill the stars.
			starImage.fillAmount = Mathf.Lerp(starImage.fillAmount, qualityScore / 100.0f, Time.deltaTime);

            // If the user presses space or the button quit the game.
            if (Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
            {
                Application.Quit();
            }
        }
	}

    // Checks if the player has reached their set required breaths.
    public bool IsSessionFinished()
    {
        if (AnalyticsManager.GetTotalBreaths() >= requiredBreaths && !sessionOver)
        {
            return true;
        }

        return false;
    }

    // Calculate the score if the session is over and create popup if so.
	public void EndSessionScore () 
	{
        if (sessionOver)
        {
            CalculateScore();

            // Check with achievement tracker.
            AchievementTracker.BreathQualityScore_Ach(qualityScore);

            // Create popup and set it under the UI canvas.
            currentCanvas = GameObject.Find("UICanvas");
            currentPanel = Instantiate(endSessionWindowPrefab, currentCanvas.transform, false) as GameObject;

            // Set each of the text objects within the popup.
            foreach (Transform child in currentPanel.transform)
            {
                // Randomly choose a message of praise for finishing.
                if (child.name == "PraiseMessage")
                {
                    Text praiseText = child.gameObject.GetComponent<Text>();
                    string[] messages = { "Congratulations!", "Well Done!", "Great Job!" };
                    int randomMessage = Random.Range(0, messages.Length);
                    praiseText.text = messages[randomMessage];
                }

                // Set the quality score text.
                if (child.name == "ScoreText")
                {
                    Text scoreText = child.gameObject.GetComponent<Text>();
                    string score = "Overall Breath Score: " + qualityScore + "/100";
                    scoreText.text = score;
                }

                // Set the current minigames score.
                if (child.name == "MGScoreText")
                {
                    Text mgScoreText = child.gameObject.GetComponent<Text>();
                    minigameScoring = FindObjectOfType<MinigameScoring>();
                    Debug.Log("Minigame score: " + minigameScoring.scoreText.text);
                    string score = "Minigame Score: " + minigameScoring.GetScore();
                    mgScoreText.text = score;
                }

                // Initialise the stars.
                if (child.name == "StarFill")
                {
                    starFill = child.gameObject;
                    starImage = starFill.GetComponent<Image>();
                    boxDisplayed = true;
                }
            }
        }

	}

    // Calculate the score based on number of quality breaths and a degree of randomness.
	void CalculateScore () 
	{
        // Debug the number of good and total breaths.
		Debug.Log ("GOOD BREATHS: " + AnalyticsManager.GetGoodBreaths());
		Debug.Log ("TOTAL BREATHS: " + AnalyticsManager.GetTotalBreaths());

        // Calculate and debug the percentage of good quality breaths of total breaths.
		float qualityPercentage = (float)AnalyticsManager.GetGoodBreaths() / (float)AnalyticsManager.GetTotalBreaths();
		Debug.Log ("QUALITY FRACTION: " + qualityPercentage);

        // Randomly decide if random value should be added or subtracted.
		int minusMultiplier = Random.Range (0, 1);
		if (minusMultiplier == 0)
		{
			minusMultiplier = -1;
		}

        // Calculate the quality score out of 100 and add randomness.
		qualityScore = ((int)(qualityPercentage * 100)) + (Random.Range (0, 9) * minusMultiplier) ;

        //  Have a minimum quality score.
		if (qualityPercentage < 0.4f)
		{
			qualityScore = 40 + Random.Range (0, 9);
		}

        // If they have a perfect score still add another degree of randomness.
		if (qualityScore > 100) 
		{
			qualityScore = 100 - Random.Range (0, 9);
		}

        // Debug the score then tell the framework
        // TODO have the score be cumlative.
		Debug.Log ("QUALITY SCORE: " + qualityScore);
        FizzyoFramework.Instance.Achievements.PostScore(qualityScore);
    }

    // Call the end of session report.
    void OnApplicationQuit()
    {
        Debug.Log("On application quit call end session");
        AnalyticsManager.ReportEndSession(Time.time);
    }
}
