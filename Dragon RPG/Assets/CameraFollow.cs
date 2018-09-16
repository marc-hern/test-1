using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject player; // Public variable to reference player GameObject

	// private Vector3 offset; // Private variable to store offset distance between player & camera

	// Use this for initialization
	void Start () {
		// Calc and store offset by getting distance between player pos & camera pos
		// offset = transform.position - player.transform.position;

		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// LateUpdate is called after Update each frame
	void LateUpdate(){
		// transform.position = player.transform.position + offset;

		transform.position = player.transform.position;
	}
}
