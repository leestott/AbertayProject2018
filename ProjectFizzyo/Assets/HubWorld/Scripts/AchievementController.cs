using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class AchievementController : MonoBehaviour
{
	public string achievementId = "a8705928-6416-45ac-939f-cda6db6ab274";
	public string userId = "c19d43d0-8546-4d7a-aaf3-aea686d9b62d";

	public int totalScore = 10;

	void Start () 
	{
		//FizzyoFramework.Instance.Achievements.UnlockAchievement (achievementId);
		FizzyoFramework.Instance.User.UserID = userId;
		FizzyoFramework.Instance.User.Login ();
		FizzyoFramework.Instance.Achievements.PostScore(totalScore);
	}
}
