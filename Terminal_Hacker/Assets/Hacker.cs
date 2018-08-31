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

	string password;
	string[] lvl1Passwords = {"password", "home", "123"};
	string[] lvl2Passwords = {"pw123", "startupPW", "123!@#"};
	string[] lvl3Passwords = {"Goog1!", "remote1!", "1!2@3#"};
	const string menuHint = "Press menu at any time...";

	List<string> quitWords = new List<string>() {
		"quit",
		"close",
		"exit"
	};
	


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
		} else if (quitWords.Contains(input)) {
			Terminal.WriteLine("Shutting down...");
			Application.Quit();
		} else if (currentScreen == Screen.MainMenu){
			RunMainMenu(input);
		} else if (currentScreen == Screen.Password){
			CheckPassword(input);
		}
	}

	void RunMainMenu(string input){
		if (input =="1" || input == "2" || input == "3"){
			level = int.Parse(input);
			StartGame();
		} else if (input == "007") {
			Terminal.WriteLine("Select a level Mr. Bond.");
			currentScreen = Screen.MainMenu;
		} else {
			Terminal.WriteLine("Invalid choice.");
			Terminal.WriteLine(menuHint);
			currentScreen = Screen.MainMenu;
		}
	}

	void StartGame(){
		currentScreen = Screen.Password;
		Terminal.ClearScreen();
		SetRandomPassword();
		Terminal.WriteLine("Enter level " + level + " password(" + password.Anagram() + "):");
		Terminal.WriteLine(menuHint);
	}

	void SetRandomPassword(){
		switch(level){
			case 1:
				password = lvl1Passwords[Random.Range(0, lvl1Passwords.Length)];
				break;
			case 2:
				password = lvl2Passwords[Random.Range(0, lvl2Passwords.Length)];
				break;
			case 3:
				password = lvl3Passwords[Random.Range(0, lvl3Passwords.Length)];
				break;
		}
	}
	
	void CheckPassword(string input) {
		if (input == password){
			DisplayWinScreen();
		} else {
			Terminal.WriteLine("Try again...");
		}
	}

	void DisplayWinScreen(){
		currentScreen = Screen.Win;

		Terminal.ClearScreen();
		Terminal.WriteLine("I'm in...");

		ShowLevelReward();
		Terminal.WriteLine(menuHint);
		
	}

	void ShowLevelReward(){
		switch(level){
			case 1:
				Terminal.WriteLine(@"
 .--#.
/ \   \
|_|___|
"
				);
				break;
			case 2:
				Terminal.WriteLine(@"
 _____________
|      __     |
|  #  |  | #  |
|_____|_ |____|
"
				);
				break;

			case 3:
				Terminal.WriteLine(@"
  ___                _
 / __|___  ___  __ _| |___
| (_ / _ \/ _ \/ _` |   -_)
 \___\___/\___/\__, |_\___|
               |___/
"
				);
				break;
		}
	}

	// Update is called once per frame
	void Update() {

	}
}
