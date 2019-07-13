using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	public float shakeDuration = 0.5f;
	float shake = 0.0f;
	public float shakeMagnitude = 0.15f;
	float dampingSpeed = 1.0f;

	Vector3 originalPosition;

    private GameObject setInfo;
    public SetDisplayInfo setDisplayInfo;

	void Start () 
	{
		originalPosition = transform.position;
        setDisplayInfo = FindObjectOfType<SetDisplayInfo>();
        DebugManager.SendDebug("The popup is displayed: " + setDisplayInfo.GetIsPopupDisplayed(), "BreathBar");
    }

	void Update () 
	{
		if ((shake > 0) && !(setDisplayInfo.GetIsPopupDisplayed()))
		{
			// Move screen by point within a circle whose radius is determined by the shake magnitude
			Vector2 shakeValue = Random.insideUnitCircle * shakeMagnitude;
			transform.localPosition = new Vector3 (shakeValue.x, shakeValue.y, transform.localPosition.z);
			//Decrement shake value until shake completed
			shake -= Time.deltaTime * dampingSpeed;
		}
		else 
		{
			//Reset screen position
			shake = 0.0f;
			transform.position = originalPosition;
		}
	}

	public void ShakeScreen () 
	{
		shake = shakeDuration;
	}
}
