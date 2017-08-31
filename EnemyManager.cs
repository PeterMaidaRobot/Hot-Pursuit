using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;                // The enemy prefab to be spawned.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public Text heatText;

	private float spawnTime = 10f;           // How long between each spawn.
	private int police = 1;  // Adjust to zero later**


	void Start ()
	{
		SetHeatText ();
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		police++;
		SetHeatText ();
	}

	void SetHeatText() {
		heatText.text = "Heat: " + police;
	}
}
