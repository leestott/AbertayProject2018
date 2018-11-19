using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fizzyo;

public class SceneChanger : MonoBehaviour
{
    // Icon and fill amount of that icon which shows key hold.
    public Image holdingIcon;
    private float fillAmount;

    private void Start()
    {
        fillAmount = holdingIcon.fillAmount;
    }

    private void Update()
    {
        Debug.Log(Input.GetJoystickNames());

        // Whilst holding the key increase UI elements fill.
		if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            fillAmount += 0.01f;
        }
        else
        {
            fillAmount = 0.0f;
        }

        // Only show the icon when held for certain amount of time.
        if (fillAmount >= 0.3)
        {
            holdingIcon.fillAmount = fillAmount;
        }
        else
        {
            holdingIcon.fillAmount = 0.0f;
        }

        // If UI bar fills up select that minigame.
        if(fillAmount >= 1)
        {
            // TO DO: Remove debug code of automatically going to blowdart game
            // Using a getter to retrieve current minigame selected.
            //sceneChanger(GetComponent<UISelector>().getSelected());
            sceneChanger(2);
        }
    }

    private void sceneChanger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
