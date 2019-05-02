﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	GameObject startPromptText;
	GameObject characterMenu;

	CannonController controller;

	public int currentMenuIndex;

	public GameObject[] characterPanels;
	public Transform[] menuPositions;
	public Vector3 currentMenuPosition;
	public GameObject menuTest;

	public float rotateSpeed;

	float pressTime;

	public GameObject breathBar;
	public BreathMetre breathMetre;

	private bool breathBegin = false;
	float fillAmount = 0.0f;

	public Image loadingIcon;

	void Start () 
	{
		StaticGameState.currentGameState = StaticGameState.GameState.CharacterMenu;
		startPromptText = GameObject.Find ("StartPromptText");
		characterMenu = GameObject.Find ("CharacterPanels");

		controller = GameObject.FindObjectOfType<CannonController> ();

		startPromptText.SetActive (false);
		characterMenu.SetActive (false);

		FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
		FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;

		currentMenuIndex = 0;

		pressTime = 0;
	}

	void Update () 
	{
		switch (StaticGameState.currentGameState) 
		{
		case StaticGameState.GameState.CharacterMenu:
			startPromptText.SetActive (false);
			breathBar.SetActive (false);
			characterMenu.SetActive (true);
			loadingIcon.gameObject.SetActive (true);
			CharacterMenu ();
			break;
		case StaticGameState.GameState.StartPrompt:
			startPromptText.SetActive (true);
			breathBar.SetActive (false);
			characterMenu.SetActive (false);
			loadingIcon.gameObject.SetActive (false);
			StartPrompt (); 
			break;
		case StaticGameState.GameState.Gameplay:
			startPromptText.SetActive (false);
			breathBar.SetActive (true);
			characterMenu.SetActive (false);
			loadingIcon.gameObject.SetActive (false);
			Gameplay ();
			break;
		}
	}

	void CharacterMenu () 
	{
		if (breathBegin) {
			fillAmount = breathMetre.fillAmount;
		}

		loadingIcon.fillAmount = fillAmount;

		if (fillAmount >= 1.0f) 
		{
			StaticGameState.currentGameState = StaticGameState.GameState.StartPrompt;
			Debug.Log ("CHARACTER SELECTED: " + StaticGameState.currentCharacter);
		}
		if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && pressTime >= 1.2f)
		{
			currentMenuIndex++;
			if (currentMenuIndex > menuPositions.Length - 1)
			{
				currentMenuIndex = 0;
			}
			pressTime = 0;
		}

		for (int i = 0; i < characterPanels.Length; i++) 
		{
			int menuOffset = i + currentMenuIndex;
			if (menuOffset > menuPositions.Length - 1) 
			{
				menuOffset -= menuPositions.Length;
			}
				
			characterPanels [i].transform.position = Vector3.Lerp (characterPanels [i].transform.position, menuPositions [menuOffset].position, Time.deltaTime * rotateSpeed);
			characterPanels [i].transform.localScale = Vector3.Lerp (characterPanels [i].transform.localScale, menuPositions [menuOffset].localScale, Time.deltaTime * rotateSpeed);
		}

		pressTime += Time.deltaTime;

		switch (currentMenuIndex) 
		{
		case 0:
			StaticGameState.currentCharacter = StaticGameState.CharacterState.Alien;
			break;
		case 1:
			StaticGameState.currentCharacter = StaticGameState.CharacterState.Unicorn;
			break;
		case 2:
			StaticGameState.currentCharacter = StaticGameState.CharacterState.Bigfoot;
			break;
		case 3:
			StaticGameState.currentCharacter = StaticGameState.CharacterState.Bear;
			break;
		case 4:
			StaticGameState.currentCharacter = StaticGameState.CharacterState.Batboy;
			break;
		default:
			break;
		}
	}

	void StartPrompt() 
	{
		if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()))
		{
			breathMetre.reset = true;
			breathMetre.fillAmount = 0.0f;
			controller.Reset ();
			StaticGameState.currentGameState = StaticGameState.GameState.Gameplay;
			Debug.Log ("BREATH FILL AMOUNT:" + breathMetre.fillAmount);
		}
	}

	void Gameplay() 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			StaticGameState.currentGameState = StaticGameState.GameState.CharacterMenu;

			GameObject projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
			if (projectile != null) 
			{
				GameObject.Destroy (projectile);
			}
			controller.Reset ();
		}
	}
	
	void OnBreathStarted(object sender)
	{
		breathBegin = true;
	}

	// Function called when breath ends. Reset the fill bar.
	void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
	{
		fillAmount = 0.0f;
		breathBegin = false;
		if (StaticGameState.currentGameState == StaticGameState.GameState.CharacterMenu)
		{
			breathMetre.reset = true;
		}
	}

}
