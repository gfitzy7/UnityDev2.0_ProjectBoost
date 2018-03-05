using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	[SerializeField] float boostStrength = 1.0f;
	[SerializeField] float rotationSpeed = 100.0f;

	public ParticleSystem leftBoostParticles;
	public ParticleSystem rightBoostParticles;

	Rigidbody rigidBody;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Thrust();
		Rotate();

		Camera camera = FindObjectOfType<Camera>();
		camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, camera.transform.position.z);
	}

	void OnCollisionEnter(Collision collision){
		string tag = collision.gameObject.tag;
		if(tag == "Fuel"){

		}
		else if(tag != "Friendly"){
			print("dead");
		}
	}

	private void Thrust(){
		if(Input.GetKey(KeyCode.Space)){

			rigidBody.AddRelativeForce(Vector3.up * boostStrength);

			if(!leftBoostParticles.isEmitting){
				leftBoostParticles.Play();
				rightBoostParticles.Play();
			}

			if(!audioSource.isPlaying){
				audioSource.Play();
			}
		}
		else if(leftBoostParticles.isPlaying){
			if(leftBoostParticles.isPlaying){
				leftBoostParticles.Stop();
				rightBoostParticles.Stop();
			}

			if(audioSource.isPlaying){
				audioSource.Stop();
			}
		}
	}

	private void Rotate(){
		rigidBody.angularVelocity = Vector3.zero;

		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			HandleRotation(1);
		}
		else if(!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
			HandleRotation(-1);
		}
	}

	private void HandleRotation(float scale){
		transform.Rotate(Vector3.forward * scale * Time.deltaTime * rotationSpeed);
	}
}