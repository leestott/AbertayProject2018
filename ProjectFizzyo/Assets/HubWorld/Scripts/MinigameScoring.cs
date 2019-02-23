using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScoring : MonoBehaviour {

    public Text scoreText;
    private int score;

    public Text multiplierText;
    private int multiplier;

    private BreathMetre breatheMetre;

    public int GetScore() { return score; }

    void Start()
    {
        score = 0;
        multiplier = 1;
        breatheMetre = FindObjectOfType<BreathMetre>();
    }

    void Update ()
    {
        multiplier = breatheMetre.scoreMultiplier;

        scoreText.text = "Score: " + score;
        multiplierText.text = "Multiplier x" + multiplier;
	}

    public void AddScore(int scoreAdded)
    {
        score += scoreAdded * multiplier;
    }
}
