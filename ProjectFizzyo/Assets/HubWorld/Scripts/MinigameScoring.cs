using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScoring : MonoBehaviour {

    // Reference to the text and the value for the minigame score
    public Text scoreText;
    private int score;

    // Reference to the text and the value for the minigame multiplier
    public Text multiplierText;
    private int multiplier;

    // Reference to the breath metre.
    private BreathMetre breatheMetre;

    // Getter for the score.
    public int GetScore() { return score; }

    // Initalise the variables and find the breath metre.
    void Start()
    {
        score = 0;
        multiplier = 1;
        breatheMetre = FindObjectOfType<BreathMetre>();
    }

    void Update ()
    {
        // Get the multplier from the breath metre
        multiplier = breatheMetre.scoreMultiplier;

        // Update the text.
        scoreText.text = "Score: " + score;
        multiplierText.text = "Multiplier x" + multiplier;
	}

    // Add the score.
    public void AddScore(int scoreAdded)
    {
        score += scoreAdded * multiplier;
    }
}
