using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;


	void Start() {
		StartCoroutine (SpawnWaves ());
	}

	// puts it on it's own thread
	IEnumerator SpawnWaves() {

		for (int i = 0; i < hazardCount; i++) {
			float x = Random.Range (-spawnValues.x, spawnValues.x);
			Vector3 spawnPosition = new Vector3 (x, spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (hazard, spawnPosition, spawnRotation);

			// wait time between calls
			yield return new WaitForSeconds(spawnWait);
		}
	}

}
