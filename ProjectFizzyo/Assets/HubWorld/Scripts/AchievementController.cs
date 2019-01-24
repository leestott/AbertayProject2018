using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class AchievementController : MonoBehaviour
{
	public string achievementId = "a8705928-6416-45ac-939f-cda6db6ab274";

	void Start () 
	{
		FizzyoFramework.Instance.Achievements.UnlockAchievement (achievementId);
	}
}
