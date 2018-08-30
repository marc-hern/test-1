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
	Screen currentScreen;

	// Passwords
	// Array version
	string password;
	string[] lvl1Passwords = {"password", "home", "123"};
	string[] lvl2Passwords = {"pw123", "startupPW", "123!@#"};
	string[] lvl3Passwords = {"Goog1!", "remote1!", "1!2@3#"};
	// List version
	// List<string> lvl1Passwords = new List<string>() {
	// 	"password",
	// 	"home",
	// 	"123"
	// };
	// List<string> lvl2Passwords = new List<string>() {
	// 	"pw123",
	// 	"startupPW",
	// 	"123!@#"
	// };
	// List<string> lvl3Passwords = new List<string>() {
	// 	"Goog1!",
	// 	"remote1!",
	// 	"1!2@3#"
	// };


	// Use this for initialization
	void Start() {
		ShowMainMenu();
	}

	void ShowMainMenu() {
		currentScreen = Screen.MainMenu;
		Terminal.ClearScreen();
		Terminal.WriteLine("Hack locations: ");
		Terminal.WriteLine("1.  Local");
		Terminal.WriteLine("2.  Startup");
		Terminal.WriteLine("3.  Google");
		Terminal.WriteLine("Enter selection:");
	}

	void OnUserInput(string input) {
		if (input.ToLower() == "menu") {
			ShowMainMenu();
		} else if (currentScreen == Screen.MainMenu){
			RunMainMenu(input);
		} else if (currentScreen == Screen.Password){
			CheckPassword(input);
		}
	}

	void RunMainMenu(string input){
		if (input =="1" || input == "2"){
			level = int.Parse(input);
			StartGame();
		} else if (input == "007") {
			Terminal.WriteLine("Select a level Mr. Bond.");
			currentScreen = Screen.MainMenu;
		} else {
			Terminal.WriteLine("Invalid choice.");
			currentScreen = Screen.MainMenu;
		}
	}

	void StartGame(){
		currentScreen = Screen.Password;
		Terminal.ClearScreen();
		switch(level){
			case 1:
				password = lvl1Passwords[Random.Range(1, 3)];
				break;
			case 2:
				password = lvl2Passwords[Random.Range(1, 3)];
				break;
			case 3:
				password = lvl3Passwords[Random.Range(1, 3)];
				break;
		}
		Terminal.WriteLine("Enter level " + level + " password:");
	}
	
	void CheckPassword(string input) {
		if (input == password){
			Terminal.WriteLine("I'm in...");
		} else {
			Terminal.WriteLine("Try again...");
		}
		// if (level == 1){
		// 	if (lvl1Passwords.Contains(input)) {
		// 		Terminal.WriteLine("I'm in...");
		// 	}
		// 	else {
		// 		Terminal.WriteLine("Try again...");
		// 	}
		// } else if (level == 2){
		// 	if (lvl2Passwords.Contains(input)) {
		// 		Terminal.WriteLine("I'm in...");
		// 	}
		// 	else {
		// 		Terminal.WriteLine("Try again...");
		// 	}
		// } else if (level == 3){
		// 	if (lvl3Passwords.Contains(input)) {
		// 		Terminal.WriteLine("I'm in...");
		// 	}
		// 	else {
		// 		Terminal.WriteLine("Try again...");
		// 	}
		// }
	}
	// Update is called once per frame
	void Update() {
		
	}
}
