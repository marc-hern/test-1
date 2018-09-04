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
	[SerializeField]float levelLoadDelay = 2f;

	[SerializeField]AudioClip mainEngine;
	[SerializeField]AudioClip deathSound;
	[SerializeField]AudioClip levelCompleteSound;
	
	[SerializeField]ParticleSystem mainEngineParticles;
	[SerializeField]ParticleSystem deathParticles;
	[SerializeField]ParticleSystem successParticles;

	bool isTransitioning = false;

	bool collisionsAreDisabled = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTransitioning){
			RespondToThrustInput();
			RespondToRotateInput();
		}

		if (Debug.isDebugBuild){
			RespondToDebugKeys();
		}
	}

	private void RespondToDebugKeys(){
		if (Input.GetKeyDown(KeyCode.L)){
			LoadNextScene();
		}
		if (Input.GetKeyDown(KeyCode.C)){
			collisionsAreDisabled = !collisionsAreDisabled;
		}
	}

	private void RespondToThrustInput(){
		if (Input.GetKey(KeyCode.Space)){
			ApplyThrust();
		} else {
			StopApplyingThrust();
		}
	}

	private void StopApplyingThrust(){
		audioSource.Stop();
		mainEngineParticles.Stop();
	}

	private void ApplyThrust(){
		// TODO (Marcus): Possible remove Time.deltaTime for movement issues
		// rigidBody.AddRelativeForce(Vector3.up * mainThrust);
		rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!audioSource.isPlaying){
			audioSource.PlayOneShot(mainEngine);
		}
		mainEngineParticles.Play();
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
		rigidBody.angularVelocity = Vector3.zero;
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.forward * rotationThisFrame);
		} else if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}

		rigidBody.freezeRotation = false;
	}

	void OnCollisionEnter(Collision collision){
		if (isTransitioning || collisionsAreDisabled) { return; }
		
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
		successParticles.Play();
		isTransitioning = true;
		Invoke("LoadNextScene", levelLoadDelay);
	}

	private void StartDeathSequence(){
		audioSource.Stop();
		audioSource.PlayOneShot(deathSound);
		deathParticles.Play();
		isTransitioning = true;
		Invoke("LoadFirstScene", levelLoadDelay);
	}

	private void LoadNextScene(){
		int totalScenes = SceneManager.sceneCountInBuildSettings;
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		if (currentSceneIndex == (totalScenes - 1)){
			SceneManager.LoadScene(0);
		}
		else {
			int nextSceneIndex = currentSceneIndex + 1;
			SceneManager.LoadScene(nextSceneIndex);
		}
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
