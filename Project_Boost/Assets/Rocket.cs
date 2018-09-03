using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;
	bool soundPlaying = false;
	float startVolume = 0.101f;
	[SerializeField]float rcsThrust = 150f;
	[SerializeField]float mainThrust = 20f;
	[SerializeField]AudioClip mainEngine;
	[SerializeField]AudioClip deathSound;
	[SerializeField]AudioClip levelCompleteSound;

	enum State {Alive, Dying, Transcending};
	State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Alive){
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	private void RespondToThrustInput(){
		if (Input.GetKey(KeyCode.Space)){
			ApplyThrust();
		} else {
			audioSource.Stop();
		}
	}

	private void ApplyThrust(){
		rigidBody.AddRelativeForce(Vector3.up * mainThrust);
		if (!audioSource.isPlaying){
			audioSource.PlayOneShot(mainEngine);
		}
	}

	// private void Thrust() {
	// 	if (Input.GetKey(KeyCode.Space)) {
	// 		rigidBody.AddRelativeForce(Vector3.up * mainThrust);
	// 		if (!soundPlaying){
	// 			soundPlaying = true;
	// 			audioSource.PlayOneShot(mainEngine);
    //             audioSource.volume = startVolume;
    //             audioSource.UnPause();
	// 		}
	// 	} else {
	// 		if (soundPlaying){
	// 			soundPlaying = false;
    //             StartCoroutine(VolumeFade(audioSource, 0f, 0.05f));
	// 		}
	// 		// audioSource.Stop();
	// 	}
	// }

	private void RespondToRotateInput() {

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
		if (state != State.Alive) { return; }
		
		switch(collision.gameObject.tag){
			case "Friendly":
				break;
			
			case "Finish":
				StartSuccessSequence();
				break;

			default:
				StartDeathSequence();
				break;
		}
	}

	private void StartSuccessSequence(){
		audioSource.Stop();
		audioSource.PlayOneShot(levelCompleteSound);
		state = State.Transcending;
		Invoke("LoadNextScene", 1f);
	}

	private void StartDeathSequence(){
		audioSource.Stop();
		audioSource.PlayOneShot(deathSound);
		state = State.Dying;
		Invoke("LoadFirstScene", 1f);
	}

	private void LoadNextScene(){
		SceneManager.LoadScene(1);
	}

	private void LoadFirstScene(){
		SceneManager.LoadScene(0);
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
