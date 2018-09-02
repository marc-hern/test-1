using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;
	bool soundPlaying = false;
	float startVolume = 0.101f;
	[SerializeField]float rcsThrust = 150f;
	[SerializeField]float mainThrust = 20f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Thrust();
		Rotate();
	}

	private void Thrust() {
		if (Input.GetKey(KeyCode.Space)) {
			rigidBody.AddRelativeForce(Vector3.up * mainThrust);
			if (!soundPlaying){
				soundPlaying = true;
				audioSource.Play();
                audioSource.volume = startVolume;
                audioSource.UnPause();
			}
		} else {
			if (soundPlaying){
				soundPlaying = false;
                StartCoroutine(VolumeFade(audioSource, 0f, 0.05f));
			}
			// audioSource.Stop();
		}
	}

	private void Rotate() {

		rigidBody.freezeRotation = true;
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.forward * rotationThisFrame);
		} else if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}

		rigidBody.freezeRotation = false;
	}

	void OnCollisionEnter(Collision collision){
		switch(collision.gameObject.tag){
			case "Friendly":
				print("OK");
				break;
			
			case "Fuel":
				print("gas gas gas");
				break;

			default:
				print("you ded");
				break;
		}
	}

	IEnumerator VolumeFade(AudioSource _AudioSource, float _EndVolume, float _FadeLength)
    {
        float _StartTime = Time.time;
        while (!soundPlaying &&
               Time.time < _StartTime + _FadeLength)
        {
            _AudioSource.volume = startVolume + ((_EndVolume - startVolume) * ((Time.time - _StartTime) / _FadeLength));
            yield return null;
        }
        if (_EndVolume == 0) { _AudioSource.UnPause(); }
	}
}
