using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class UISelector : MonoBehaviour {

    // Each icon accosiated with each minigame.
    [Header("Each Minigame Object:")]
    public GameObject[] minigames;

    // Each name corresponding to each minigame.
    [Header("Each Minigame Name:")]
    public string[] minigameNames;

    // Text displaying minigame name.
    [Header("Minigame Name Text Object:")]
    public Text minigameName;

    // Camera variables.
    [Header("Main Camera:")]
    public Camera mainCamera;
    private Vector3 newCamPos;

    // Private variable which represents selected minigames, access with getters.
    private int selected = 1;

    // Starting value for the Lerp.
    static float t = 0.0f;
    // Animate the game object to scale from minimum to maximum and back.
    private float minimum = 0.0F;
    private float maximum = 0.1F;

void Start()
    {
        newCamPos = minigames[selected - 1].transform.position;
        minigameName.text = minigameNames[selected - 1];
    }

	void Update ()
    {
        WhichGame();
        MoveCamera(mainCamera.transform.position, newCamPos);
    }

    // Choose which minigame
    private void WhichGame()
    {
        // On button press change minigame selected.
		if (Input.GetKeyUp(KeyCode.Space)|| FizzyoFramework.Instance.Device.ButtonDown())
        {
            //Debug.Log("The button was pressed.");

            selected++;

            // Wrap selected around.
            if (selected > 3)
            {
                selected = 1;
            }

            newCamPos = minigames[selected - 1].transform.position;
            minigameName.text = minigameNames[selected - 1];
        }

        ProportionalScale(minigames[selected - 1]);
    }

    // Causes object to pulse.
    private void ProportionalScale(GameObject currentMinigame)
    {
        // Increase the t interpolater for lerp using deltatime.
        t += 1.0f * Time.deltaTime;

        // When interpolater reaches one swap max and min to scale other way.
        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }

        // Scale based on lerp.
        currentMinigame.transform.localScale = new Vector3(1 + Mathf.Lerp(minimum, maximum, t), 1 + Mathf.Lerp(minimum, maximum, t), 1 + Mathf.Lerp(minimum, maximum, t));
    }

    // Lerp camera towards vector
    private void MoveCamera(Vector3 currentPos, Vector3 newPos)
    {
        mainCamera.transform.position = new Vector3(Mathf.Lerp(currentPos.x, newPos.x, 0.12f), Mathf.Lerp(currentPos.y, newPos.y, 0.12f), -10.0f) ;
    } 

    // Getter for selected variable.
    public int getSelected()
    {
        return selected;
    }
}
