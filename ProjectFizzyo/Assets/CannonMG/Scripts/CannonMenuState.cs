using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fizzyo;

public class CannonMenuState : MonoBehaviour {

	public enum GameState {CharacterSelect, StartPrompt ,Gameplay};

	public GameState gameState;

	public GameObject[] menuItems;

	GameObject characterGrid;
	GameObject characterSelectionPanel;
	GameObject breathMetre;

	CannonController controller;

	float menuDistance = 0.0f;
	int menuIndex = 0;

	private GameObject startPromptText;

	public Image holdingIcon;
	public float fillAmount;

	// Breath pressure and whether the breath has began.
	private float breathPressure = 0;
	private bool breathBegin = false;

	private float breathTime = 0.0f;

	void Start () 
	{
		characterSelectionPanel = GameObject.Find ("CharacterCanvas");
		characterGrid = GameObject.Find ("CharacterGrid");
		breathMetre = GameObject.Find ("BreathMetre");
		startPromptText = GameObject.Find ("StartPromptText");
		gameState = GameState.CharacterSelect;

		CannonStaticValues.playerCharacter = CannonStaticValues.Characters.Alien;

		controller = GameObject.FindObjectOfType<CannonController> ();

		menuDistance = menuItems [1].transform.position.x + menuItems [0].transform.position.x;

		fillAmount = holdingIcon.fillAmount;

		// Links up the breath started and breath complete functions.
		FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
		FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
	}

	void Update () 
	{
		if (gameState == GameState.CharacterSelect)
		{
			characterSelectionPanel.SetActive (true);
			breathMetre.SetActive (false);
			controller.gameplayState = false;
			startPromptText.SetActive (false);
		}
		else if (gameState == GameState.Gameplay)
		{
			characterSelectionPanel.SetActive (false);
			breathMetre.SetActive (true);
			controller.gameplayState = true;
			startPromptText.SetActive (false);
		}
		else if (gameState == GameState.StartPrompt) 
		{
			characterSelectionPanel.SetActive (false);
			breathMetre.SetActive (false);
			startPromptText.SetActive (true);
			controller.gameplayState = false;
		}

		if (Input.GetKeyDown (KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown ())
		{
			if (gameState == GameState.CharacterSelect) {

				menuIndex++;
				characterGrid.transform.position += new Vector3 (menuDistance, 0, 0);
				if (menuIndex > menuItems.Length - 1) {
					characterGrid.transform.position -= new Vector3 (menuDistance * menuIndex, 0, 0);
					menuIndex = 0;
				}
			} 
			else if (gameState == GameState.StartPrompt) 
			{
				gameState = GameState.Gameplay;
			}
				
		}

		if (gameState == GameState.CharacterSelect) {

			breathPressure = FizzyoFramework.Instance.Device.Pressure ();

			// If the breath has began then increase the fill amount based on the pressure.
			if (breathBegin) {
				// Scale the breath pressure down a bit for filling icon.
				breathTime += Time.deltaTime;
				fillAmount = breathTime / 1;
			}

			// Only show the icon when held for certain amount of time.
			holdingIcon.fillAmount = fillAmount;

			// If UI bar fills up select that minigame. Or Spacebar will select for debug mode.

			if (fillAmount >= 1) {
				// TOOD: Add loading game
				Debug.Log ("GAME START");
				gameState = GameState.StartPrompt;

				BreathMetre metreScipt = GameObject.FindObjectOfType<BreathMetre> ();
				metreScipt.reset = true;

				switch (menuItems [menuIndex].name) 
				{
				case "Alien":
					CannonStaticValues.playerCharacter = CannonStaticValues.Characters.Alien;
					break;
				case "Batboy":
					CannonStaticValues.playerCharacter = CannonStaticValues.Characters.Batboy;
					break;
				case "Bear":
					CannonStaticValues.playerCharacter = CannonStaticValues.Characters.Bear;
					break;
				case "Unicorn":
					CannonStaticValues.playerCharacter = CannonStaticValues.Characters.Unicorn;
					break;
				case "BigFoot":
					CannonStaticValues.playerCharacter = CannonStaticValues.Characters.BigFoot;
					break;
				}
			}
		}
	}

	// Function called when breath begins.
	void OnBreathStarted(object sender)
	{
		breathTime = 0.0f;
		breathBegin = true;
	}

	// Function called when breath ends. Reset the fill bar.
	void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
	{
		fillAmount = 0.0f;
		breathBegin = false;
	}
}
