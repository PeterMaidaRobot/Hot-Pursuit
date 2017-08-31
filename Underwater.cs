using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour {

	public float waterLevel;

	private bool isUnderwater = false;
	private Color normalColor;
	private Color underwaterColor;

	// Use this for initialization
	void Start () {
		normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
		underwaterColor = new Color (0.22f, 0.65f, 0.77f, 0.3f); //(0.22f, 0.65f, 0.77f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.y < waterLevel) != isUnderwater){
			isUnderwater = transform.position.y < waterLevel;
			if (isUnderwater) {
				SetUnderwater ();
			} else {
				SetNormal ();
			}
		}
			
	}
	void SetUnderwater() {
		RenderSettings.fogColor = underwaterColor;
		RenderSettings.fogDensity = 0.1f;
	}
	void SetNormal() {
		RenderSettings.fogColor = normalColor;
		RenderSettings.fogDensity = 0.002f;
	}
}
