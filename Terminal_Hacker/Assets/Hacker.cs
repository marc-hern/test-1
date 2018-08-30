using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// Game state
	int level;

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
			ShowMainMenu();
		} else if (input == "007") {
			Terminal.WriteLine("Select a level Mr. Bond.");
		} else if (input == "1") {
			level = 1;
			StartGame();
		} else if (input == "2") {
			level = 2;
			StartGame();
		} else {
			Terminal.WriteLine("Invalid choice.");
		}
	}

	void StartGame(){
		Terminal.WriteLine("You have chosen level " + level);
	}
	
	// Update is called once per frame
	void Update() {
		
	}
}
