using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// Use this for initialization
	void Start() {
		ShowMainMenu("Logged in...");
	}

	void ShowMainMenu(string logIn) {
		Terminal.ClearScreen();

		Terminal.WriteLine(logIn);
		Terminal.WriteLine("Hack locations: ");
		Terminal.WriteLine("1.  Local");
		Terminal.WriteLine("1.  Startup");
		Terminal.WriteLine("1.  Google");
		Terminal.WriteLine("Enter selection:");
	}

	void OnUserInput(string input){
		print(input);
	}
	
	// Update is called once per frame
	void Update() {
		
	}
}
