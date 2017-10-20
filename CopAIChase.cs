using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CopAIChase : MonoBehaviour {

	public Transform target;
	public GameObject redLight, blueLight;

	private float maxSpeed = 20.0f;//was 25
	private Rigidbody rigidbody;
	private int flashingLight = 0; // 0 for off, 1 for red, 2 for blue
	private float nextLightTime = 0.0f;
	private float period = 0.5f;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.maxAngularVelocity = 4;
		flashingLight = 1;
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			if (flashingLight == 1 && Time.time > nextLightTime) {
				// turns on red light and off blue light
				redLight.SetActive(true);
				blueLight.SetActive (false);
				flashingLight = 2;
				nextLightTime += period;
			} else if (flashingLight == 2 && Time.time > nextLightTime) {
				// turns on blue light and off red light
				blueLight.SetActive (true);
				redLight.SetActive(false);
				flashingLight = 1;
				nextLightTime += period;
			}
		}
	}

	void FixedUpdate() {
		// Non Physics :(
		//float step = maxSpeed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, target.position, step);

		//Physics :)
		/*
		Vector3 lookPos = target.position - transform.position; // use transform.forward;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookPos);
		int magnitude = 20;
		print (lookPos + "\n" + rigidbody.transform.forward + "\n" + transform.right + "\n" + transform.up + "\n");
		if (true) {
			rigidbody.AddTorque (-transform.up * magnitude);
		} else {
			rigidbody.AddTorque (transform.up * magnitude);
		}
		*/
		/*
		var lookPos = target.position - transform.position;
		var x = Vector3.Cross(transform.forward.normalized, lookPos.normalized);
		float theta = Mathf.Asin(x.magnitude);
		var w = x.normalized * theta / Time.fixedDeltaTime;
		var q = transform.rotation * rigidbody.inertiaTensorRotation;
		var t = q * Vector3.Scale(rigidbody.inertiaTensor, Quaternion.Inverse(q) * w);
		rigidbody.AddTorque(t - rigidbody.angularVelocity, ForceMode.Impulse);
		*/

		//THIS SHOULD WORK!!! WORKED ON STUPID COP!

		var lookPos = target.position - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);


		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight (transform.position);
		if (terrainHeightWhereWeAre + 0.1 > transform.position.y
				&& rigidbody.velocity.magnitude < maxSpeed / 4) {
			// only drives if it is on or close to the terrain and not already going too fast
			rigidbody.AddForce (transform.forward * maxSpeed);
		}

	}
}
