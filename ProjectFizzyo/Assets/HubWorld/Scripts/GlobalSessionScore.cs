using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class GlobalSessionScore : MonoBehaviour {

	public int goodBreaths = 0;
	public int totalBreaths = 0;

	public GameObject endSessionWindowPrefab;
	GameObject currentPanel;

	GameObject currentCanvas;

	public int qualityScore = 0;

	private int currentBreaths;
	private int requiredBreaths;

	bool sessionOver = false;
	public bool boxDisplayed = false;

	GameObject starFill;
	Image starImage;

    AudioSource audioSource;
    public AudioClip achievementSound;
    private bool audioPlayed = false;

    private MinigameScoring minigameScoring;


    void Start () 
	{
		DontDestroyOnLoad (this);

        if (GameObject.Find(gameObject.name)
                     && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(GameObject.Find(gameObject.name));
        }

        requiredBreaths = AnalyticsManager.GetBreathsPerSet () * AnalyticsManager.GetTotalSets ();
		Debug.Log ("User Sets: " + AnalyticsManager.GetTotalSets ());
		Debug.Log ("User Breaths per Set: " + AnalyticsManager.GetBreathsPerSet ());
		Debug.Log ("Required Breaths: " + requiredBreaths);
	}

	void Update () 
	{
		currentBreaths = AnalyticsManager.GetTotalBreaths ();

		if (currentBreaths >= requiredBreaths && !sessionOver) 
		{
			sessionOver = true;
		}

		if (boxDisplayed) 
		{
            if(!audioPlayed)
            {
                audioSource = FindObjectOfType<AudioSource>();
                audioSource.PlayOneShot(achievementSound);
                audioPlayed = true;
            }

			starImage.fillAmount = Mathf.Lerp(starImage.fillAmount, qualityScore / 100.0f, Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
            {
                Application.Quit();
            }
        }
	}

	public void EndSessionScore () 
	{
        if (sessionOver)
        {
            goodBreaths = AnalyticsManager.GetGoodBreaths();
            totalBreaths = AnalyticsManager.GetTotalBreaths();
            CalculateScore();

            AchievementTracker.BreathQualityScore_Ach(qualityScore);

            currentCanvas = GameObject.Find("UICanvas");
            currentPanel = Instantiate(endSessionWindowPrefab, currentCanvas.transform, false) as GameObject;

            foreach (Transform child in currentPanel.transform)
            {
                if (child.name == "PraiseMessage")
                {
                    Text praiseText = child.gameObject.GetComponent<Text>();
                    string[] messages = { "Congratulations!", "Well Done!", "Great Job!" };
                    int randomMessage = Random.Range(0, messages.Length);
                    praiseText.text = messages[randomMessage];
                }
                if (child.name == "ScoreText")
                {
                    Text scoreText = child.gameObject.GetComponent<Text>();
                    string score = "Overall Breath Score: " + qualityScore + "/100";
                    scoreText.text = score;
                }
                if (child.name == "MGScoreText")
                {
                    Text mgScoreText = child.gameObject.GetComponent<Text>();
                    minigameScoring = FindObjectOfType<MinigameScoring>();
                    Debug.Log("Minigame score: " + minigameScoring.scoreText.text);
                    string score = "Minigame Score: " + minigameScoring.GetScore();
                    mgScoreText.text = score;
                }
                if (child.name == "StarFill")
                {
                    starFill = child.gameObject;
                    starImage = starFill.GetComponent<Image>();
                    boxDisplayed = true;
                }
            }
        }

	}

	void CalculateScore () 
	{
		Debug.Log ("GOOD BREATHS: " + goodBreaths);
		Debug.Log ("TOTAL BREATHS: " + totalBreaths);
		float qualityPercentage = (float)goodBreaths / (float)totalBreaths;
		Debug.Log ("QUALITY FRACTION: " + qualityPercentage);
		int minusMultiplier = Random.Range (0, 1);
		if (minusMultiplier == 0)
		{
			minusMultiplier = -1;
		}
		qualityScore = ((int)(qualityPercentage * 100)) + (Random.Range (0, 9) * minusMultiplier) ;
		if (qualityPercentage < 0.4f)
		{
			qualityScore = 40 + Random.Range (0, 9);
		}
		if (qualityScore > 100) 
		{
			qualityScore = 100 - Random.Range (0, 9);
		}

		Debug.Log ("QUALITY SCORE: " + qualityScore);
        FizzyoFramework.Instance.Achievements.PostScore(qualityScore);
    }


}
