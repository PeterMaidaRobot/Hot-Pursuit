using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireDriver : MonoBehaviour {

	private Rigidbody rigidbody;
	public WheelCollider[] wheels = new WheelCollider[4];


	public float enginePower = 150.0f;
	public float power = 0.0f;
	public float brake = 0.0f;
	public float steer = 0.0f;
	public float maxSteer = 25.0f;

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.centerOfMass = new Vector3(0.0f, -0.01f, -0.01f);
	}

	void Update() {
		power=Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
		steer=Input.GetAxis("Horizontal") * maxSteer;
		brake=Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 0.1f: 0.0f;

		GetCollider(0).steerAngle=steer;
		GetCollider(1).steerAngle=steer;

		if(brake > 0.0f){
			GetCollider(0).brakeTorque=brake;
			GetCollider(1).brakeTorque=brake;
			GetCollider(2).brakeTorque=brake;
			GetCollider(3).brakeTorque=brake;
			GetCollider(2).motorTorque=0.0f;
			GetCollider(3).motorTorque=0.0f;
		} else {
			GetCollider(0).brakeTorque=0.0f;
			GetCollider(1).brakeTorque=0.0f;
			GetCollider(2).brakeTorque=0.0f;
			GetCollider(3).brakeTorque=0.0f;
			GetCollider(2).motorTorque=power;
			GetCollider(3).motorTorque=power;
		}

	}

	WheelCollider GetCollider(int n) {
		return wheels[n];
	}

}



	/*
	public float maxTorque = 50f;
	public float steerForce = 2f;

	//public float motorForce, steerForce;
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tireMeshes = new Transform[4];



	public void UpdateMeshesPositions() {

		for (int i = 0; i < 4; i++) {
			Quaternion quat;
			Vector3 pos;
			wheelColliders [i].GetWorldPose (out pos, out quat);

			tireMeshes[i].position = pos;
			tireMeshes[i].rotation = quat;
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate(){

		float steer = Input.GetAxis ("Horizontal");

		float finalAngle = steer * 45f;

		wheelColliders[0].steerAngle = finalAngle;
		wheelColliders[1].steerAngle = finalAngle;

	}
	
	// Update is called once per frame
	void Update () {

		//UpdateMeshesPositions();

		//float v = Input.GetAxis ("Vertical") * motorForce;
		//float h = Input.GetAxis ("Horizontal") * steerForce;

		//RL.motorTorque = v;
		//RR.motorTorque = v;

		//FL.steerAngle = h;
		//FR.steerAngle = h;

	}

}

*/