using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarStatus : MonoBehaviour {

	public Text speedText;
	public Text healthText;
	public GameObject gameOver;

	private Rigidbody rigidbody;
	private int health = 100;
	private bool paused = false;


	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();

		health = 100;
		gameOver.SetActive(false);
		Time.timeScale = 1; 
		paused = false;

		SetSpeedText ();
		SetHealthText ();

	}
	
	// Update is called once per frame
	void Update () {
		
		SetSpeedText ();

		if (!paused) {

			// Respawn
			if (Input.GetKey (KeyCode.R)) {
				transform.position += Vector3.up;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
				transform.rotation = Quaternion.LookRotation (transform.forward, Vector3.up);
			}

			SetSpeedText ();
		} else {
			
			// Revive
			if (Input.GetKey (KeyCode.R)) {
				Time.timeScale = 0;
				paused = false;
				//Application.LoadLevel (0); 
				SceneManager.LoadScene (0);
			}
		}

	}


	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Police")) {
			health--;;
			SetHealthText();
		}
		if (health <= 0) {
			health = 0;
			SetHealthText ();
			gameOver.SetActive(true);
			Time.timeScale = 0; 
			paused = true;
		}
	}

	void SetSpeedText() {
		int s =  (int) (rigidbody.velocity.magnitude * 4.237 * 3 + 0.5); // triple the "real" speed
		speedText.text = string.Format("{0,3}", s) + "mph";
		/*
		if(s < 10) {
			speedText.text = (" " + s).ToString() + "mph";
		} else {
			speedText.text = s.ToString() + "mph";
		}
		*/
	}

	void SetHealthText() {
		healthText.text = "Health: " + string.Format ("{0,3}", health) + "%";
		/*
		if (health == 100) {
			healthText.text = ("Health: " + health.ToString () + "%");
		} else if (health >= 10) {
			healthText.text = ("Health:  " + health.ToString () + "%");
		} else {
			healthText.text = ("Health:   " + health.ToString () + "%");
		}
		*/
	}
}
