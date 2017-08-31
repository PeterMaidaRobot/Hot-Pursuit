using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDriver : MonoBehaviour {

	public Text speedText;
	public Text healthText;
	public GameObject gameOver;

	private Rigidbody rb;
	private int fuel = 100;
	private float forwardSpeed = 30.0f;
	private float backwardSpeed = 25.0f;
	//private float boostSpeed = 10.0f;
	private float turnRate = 6.0f;
	private int health = 100;
	private bool paused = false;

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		rb.maxAngularVelocity = 4;

		health = 100;
		gameOver.SetActive(false);
		Time.timeScale = 1; 
		paused = false;

		SetSpeedText ();
		SetHealthText ();
	}

	void Update()
	{
		if (!paused) {
			
			// Respawn
			if (Input.GetKey (KeyCode.R)) {
				transform.position += Vector3.up;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				transform.rotation = Quaternion.LookRotation (transform.forward, Vector3.up);
			}
			// Boost
			if (Input.GetKey (KeyCode.Space) && fuel > 0) {
				//rb.AddForce (transform.forward * boostSpeed);
				//fuel -= 5;
				// message "boosting" or lighting around borders or camera zooms out
			} else if (Input.GetKey (KeyCode.Space)) {
				;//message "out of fuel"
			}

			SetSpeedText ();
		} else {
			// Revive
			if (Input.GetKey (KeyCode.R)) {
				Time.timeScale = 0;
				paused = false;
				Application.LoadLevel (0);  
			}
		}

	}

	void FixedUpdate() 
	{
		Vector3 moveCamTo = transform.position - transform.forward + transform.up * 1/2; //?
		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight (transform.position);
		// Only drives if it is on or close to the terrain
		//if (terrainHeightWhereWeAre + 0.1 > transform.position.y) {
			// limits the speed
			if (Input.GetKey (KeyCode.UpArrow) && rb.velocity.magnitude < 2 * forwardSpeed) {
				rb.AddForce(transform.forward * forwardSpeed);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				rb.AddForce(- transform.forward * backwardSpeed);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				rb.AddTorque(-transform.up * turnRate);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				rb.AddTorque(transform.up * turnRate);
			}
		//}

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
		int s =  (int) (rb.velocity.magnitude * 4.237 + 0.5); // double the "real" speed
		if(s < 10) {
			speedText.text = (" " + s).ToString() + "mph";
		} else {
			speedText.text = s.ToString() + "mph";
		}
			
	}

	void SetHealthText() {
		if (health == 100) {
			healthText.text = ("Health: " + health.ToString () + "%");
		} else if (health >= 10) {
			healthText.text = ("Health:  " + health.ToString () + "%");
		} else {
			healthText.text = ("Health:   " + health.ToString () + "%");
		}
	}

}
// TODO : Turning animation on custom car, cops with ai to chase behind or in the future,
// smooth detailed road, terrain slowing down car and animation, lights on custom car, helicopter ai,
// boost bar with camera zoom out and lightning, water on terrain, decorate the terrain with buildings,
// cop sirens in background when wanted, peaceful music when not wanted, delta speed in CarDriver,
// cops blow up after being flipped for a while, no respawn for player if cops nearby, Day/Night cycle,
// minimap camera shows in bottom right with north and forward vector, fix text so that its stationary.