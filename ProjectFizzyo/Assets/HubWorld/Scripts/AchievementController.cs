using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class AchievementController : MonoBehaviour
{
	public AchievementData[] allAchievements;
	public AchievementData[] unlockAchievements;

	void Awake () 
	{
        DontDestroyOnLoad(this.gameObject);

        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(GameObject.Find(gameObject.name));
        }

        FizzyoFramework.Instance.Achievements.LoadAchievements();

		allAchievements = FizzyoFramework.Instance.Achievements.allAchievements;
		unlockAchievements = FizzyoFramework.Instance.Achievements.unlockedAchievements;
	}

    public void UnlockAchievement(string achievementToUnlock)
    {

        string unlockAchievement = string.Empty;
        if (FizzyoFramework.Instance.Achievements.allAchievements != null && FizzyoFramework.Instance.Achievements.allAchievements.Length > 0)
        {
            for (int i = 0; i < FizzyoFramework.Instance.Achievements.allAchievements.Length; i++)
            {
                if (FizzyoFramework.Instance.Achievements.allAchievements[i].title == achievementToUnlock)
                {
                    unlockAchievement = FizzyoFramework.Instance.Achievements.allAchievements[i].id;
                }
            }
            if (unlockAchievement != string.Empty)
            {
                FizzyoFramework.Instance.Achievements.UnlockAchievement(unlockAchievement);
            }
        }
    }
}