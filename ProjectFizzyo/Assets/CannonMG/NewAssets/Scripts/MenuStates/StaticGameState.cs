using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameState {

	//Public enums for game state and character selection
	public enum GameState {CharacterMenu, StartPrompt, Gameplay};
	public enum CharacterState {Alien, Batboy, Bigfoot, Bear, Unicorn};

	public static GameState currentGameState;
	public static CharacterState currentCharacter;
}
