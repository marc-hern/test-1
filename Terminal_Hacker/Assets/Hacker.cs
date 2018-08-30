using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// Game state
	int level;
	enum Screen {
		MainMenu,
		Password,
		Win
	};
	Screen currentScreen = Screen.MainMenu;

	// Use this for initialization
	void Start() {
		ShowMainMenu();
	}

	void ShowMainMenu() {
		Terminal.ClearScreen();
		Terminal.WriteLine("Hack locations: ");
		Terminal.WriteLine("1.  Local");
		Terminal.WriteLine("2.  Startup");
		Terminal.WriteLine("3.  Google");
		Terminal.WriteLine("Enter selection:");
	}

	void OnUserInput(string input) {
		if (input == "menu" || input == "Menu") {
			currentScreen = Screen.MainMenu;
			ShowMainMenu();
		} else if (input == "007") {
			Terminal.WriteLine("Select a level Mr. Bond.");
			currentScreen = Screen.MainMenu;
		} else if (input == "1") {
			level = 1;

			StartGame();
		} else if (input == "2") {
			level = 2;

			StartGame();
		} else {
			Terminal.WriteLine("Invalid choice.");
			currentScreen = Screen.MainMenu;
		}
	}

	void StartGame(){
		currentScreen = Screen.Password;
		Terminal.WriteLine("You have chosen level " + level);
	}
	
	// Update is called once per frame
	void Update() {
		
	}
}
