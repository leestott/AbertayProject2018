using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTracker : MonoBehaviour
{
    // The reference to the achievement controller.
    static AchievementController achievementController;

    // The three bools if the user has played the minigames.
    private static bool playedDart = false;
    private static bool playedTiki = false;
    private static bool playedCannon = false;

    // Check if they have got better in quality score than the achievement aim.
    private static int breathQualityScoreGoal = 75;

    // Tracks times they have hit each object.
    private static int numHitCoconut;
    private static int numHitMask;

    // Different levels of coin totals.
    private static int coinTotal = 0;
    private static int coinGoal1 = 1;
    private static int coinGoal2 = 50;
    private static int coinGoal3 = 500;

    // Different levels of balloon popped totals.
    private static int balloonTotal = 0;
    private static int balloonGoal1 = 10;
    private static int balloonGoal2 = 25;
    private static int balloonGoal3 = 100;

    // Find and initialise the reference to the achievement controller.
    private void Start()
    {
        achievementController = FindObjectOfType<AchievementController>();
    }

    // Deals with played minigame achievement.
    public static void PlayedMinigame_Ach(string minigameName)
    {
        // Checks if controller is initialised.
        if (achievementController == null)
        {
            Debug.Log("The achievement controller has not been initialised properly");
        }

        // Achievement for playing blow dart game for the first time.
        if(minigameName == "Temple of Bloon")
        {
            playedDart = true;
            achievementController.UnlockAchievement("Dastardly Darts");
            Debug.Log("Dart game played ach unlocked");
        }

        // Achievement for playing coconut game for the first time.
        if (minigameName == "Freaky Tiki's Coconut Shy")
        {
            playedTiki = true;
            achievementController.UnlockAchievement("Tiki Time");
            Debug.Log("Coconut game played ach unlocked");
        }

        // Achievement for playing crazy cannon game for the first time.
        if (minigameName == "Crazy Cannon")
        {
            playedCannon = true;
            achievementController.UnlockAchievement("Stunt it!");
            Debug.Log("Cannon game played ach unlocked");
        }

        // Achievement for playing all three minigames.
        if(playedDart && playedTiki && playedCannon)
        {
            achievementController.UnlockAchievement("Tour the Island");
            Debug.Log("All games played ach unlocked");
        }
    }

    // Achievement for breath quality checks if it is above the target.
    public static void BreathQualityScore_Ach(int breathQualityScore)
    {
        Debug.Log("The ach tracker knows the final quality score: " + breathQualityScore);

        if (breathQualityScore >= breathQualityScoreGoal)
        {
            achievementController.UnlockAchievement("Good Job!");
        }

        if(breathQualityScore == 100)
        {
            achievementController.UnlockAchievement("Perfect Performance");
        }
    }

    // Achievements for good quality breath totals.
    public static void GoodBreaths_Ach(int goodBreathsTotal)
    {
        Debug.Log("The ach tracker knows how many total good breaths: " + goodBreathsTotal);

        if (goodBreathsTotal >= 10)
        {
            achievementController.UnlockAchievement("Trainee");
        }

        if (goodBreathsTotal >= 50)
        {
            achievementController.UnlockAchievement("Beginner");
        }

        if (goodBreathsTotal >= 100)
        {
            achievementController.UnlockAchievement("Expert");
        }
    }

    // Achievement for number of masks and coconuts hit in a row.
    public static void HitInARow_Ach(string whatHit)
    {
        Debug.Log("The achievement tracker thinks it hit: " + whatHit);

        if(whatHit == "Miss")
        {
            numHitCoconut = 0;
            numHitMask = 0;
        }

        if (whatHit == "Mask")
        {
            numHitCoconut = 0;
            numHitMask ++;
        }

        if (whatHit == "Coconut")
        {
            numHitCoconut ++;
            numHitMask = 0;
        }

        if(numHitMask >= 5)
        {
            achievementController.UnlockAchievement("Joker");
        }

        if (numHitCoconut >= 5)
        {
            achievementController.UnlockAchievement("Coconut Genius");
        }
    }

    // Achievement tracking number of coins.
    public static void AddCoin_Ach()
    {
        coinTotal++;
        Debug.Log("The ach tracker knows this many coins have been collected: " + coinTotal);

        if (coinTotal > coinGoal1)
        {
            achievementController.UnlockAchievement("Pennies");
        }

        if (coinTotal > coinGoal2)
        {
            achievementController.UnlockAchievement("Spare Change");
        }

        if (coinTotal > coinGoal3)
        {
            achievementController.UnlockAchievement("Making Money");
        }
    }

    // Achievement for tracking number of balloons popped.
    public static void PopBalloon_Ach()
    {
        balloonTotal++;
        Debug.Log("The ach tracker knows this many balloons have been popped: " + balloonTotal);

        if (balloonTotal > balloonGoal1)
        {
            achievementController.UnlockAchievement("Nice Aim!");
        }

        if (balloonTotal > balloonGoal2)
        {
            achievementController.UnlockAchievement("Bloon Sniper");
        }

        if (balloonTotal > balloonGoal3)
        {
            achievementController.UnlockAchievement("Indiana Blowns");
        }
    }
}
