using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSessionScore : MonoBehaviour {

	public int goodBreaths = 0;
	public int totalBreaths = 0;

	public GameObject endSessionWindowPrefab;
	GameObject currentPanel;

	GameObject currentBreathCanvas;

	public int qualityScore = 0;

	private int currentBreaths;
	private int requiredBreaths;

	bool sessionOver = false;
	bool boxDisplayed = false;

	GameObject starFill;
	Image starImage;

	void Start () 
	{
		DontDestroyOnLoad (this);

		requiredBreaths = AnalyticsManager.GetBreathsPerSet () * AnalyticsManager.GetTotalSets ();
		Debug.Log ("User Sets: " + AnalyticsManager.GetTotalSets ());
		Debug.Log ("User Breaths per Set: " + AnalyticsManager.GetBreathsPerSet ());
		Debug.Log ("Required Breaths: " + requiredBreaths);
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			EndSessionScore ();
		}

		currentBreaths = AnalyticsManager.GetTotalBreaths ();
		Debug.Log ("CURRENT BREATHS: " + currentBreaths);

		if (currentBreaths >= requiredBreaths && !sessionOver) 
		{
			sessionOver = true;
			EndSessionScore ();
		}
		if (boxDisplayed) 
		{
			starImage.fillAmount = Mathf.Lerp(starImage.fillAmount, qualityScore / 100.0f, Time.deltaTime);
		}
	}

	public void EndSessionScore () 
	{
		goodBreaths = AnalyticsManager.GetGoodBreaths ();
		totalBreaths = AnalyticsManager.GetTotalBreaths ();
		CalculateScore ();

		currentBreathCanvas = GameObject.Find ("BreathBarCanvas");
		currentPanel = Instantiate (endSessionWindowPrefab, currentBreathCanvas.transform, false) as GameObject;

		foreach(Transform child in currentPanel.transform) 
		{
			if (child.name == "PraiseMessage")
			{
				Text praiseText = child.gameObject.GetComponent<Text> ();
				string[] messages = { "Congratulations!", "Well Done!", "Great Job!" };
				int randomMessage = Random.Range (0, messages.Length);
				praiseText.text = messages [randomMessage];
			}
			if (child.name == "ScoreText")
			{
				Text scoreText = child.gameObject.GetComponent<Text> ();
				string score = qualityScore + "/100";
				scoreText.text = score;
			}
			if (child.name == "StarFill") 
			{
				starFill = child.gameObject;
				starImage = starFill.GetComponent<Image> ();
				boxDisplayed = true;
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
	}


}
