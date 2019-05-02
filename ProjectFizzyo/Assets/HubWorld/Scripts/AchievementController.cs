using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class AchievementController : MonoBehaviour
{
    // Shows the achievements in inspector.
    [SerializeField]
	private AchievementData[] allAchievements;
    [SerializeField]
	private AchievementData[] unlockAchievements;

    // If not already created create a new achievmenet controller and load the achievements.
	void Awake () 
	{
        // Keep the controller when scenes change.
        DontDestroyOnLoad(this.gameObject);

        // Check if there is a duplicate and destroy if so.
        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(this);
        }

        // Load the achievements.
        FizzyoFramework.Instance.Achievements.LoadAchievements();

		allAchievements = FizzyoFramework.Instance.Achievements.allAchievements;
		unlockAchievements = FizzyoFramework.Instance.Achievements.unlockedAchievements;
	}

    // Unlock the achievement of the string of the same name.
    public void UnlockAchievement(string achievementToUnlock)
    {
        // Set the string to empty.
        string unlockAchievement = string.Empty;

        // Check if the achievements have been set up.
        if (FizzyoFramework.Instance.Achievements.allAchievements != null && FizzyoFramework.Instance.Achievements.allAchievements.Length > 0)
        {
            // Loop through all the achievements.
            for (int i = 0; i < FizzyoFramework.Instance.Achievements.allAchievements.Length; i++)
            {
                // If the achievement has the same title as the achievement we are looking for save the ID.
                if (FizzyoFramework.Instance.Achievements.allAchievements[i].title == achievementToUnlock)
                {
                    unlockAchievement = FizzyoFramework.Instance.Achievements.allAchievements[i].id;
                }
            }

            // If the unlock achievement has been given the ID unlock the achievement.
            if (unlockAchievement != string.Empty)
            {
				FizzyoFramework.Instance.Achievements.CheckAndUnlockAchievement(unlockAchievement);
            }
        }
    }
}