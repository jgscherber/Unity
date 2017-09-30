using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;



	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;
	private bool gameOver;
	private bool restart;


	void Start() {
		score = 0;
		updateScore ();
		StartCoroutine (SpawnWaves ());

		gameOver = false;
		restart = false;

		gameOverText.text = "";
		restartText.text = "";
	}

	void Update() {
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	// puts it on it's own thread
	IEnumerator SpawnWaves() {

		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				float x = Random.Range (-spawnValues.x, spawnValues.x);
				Vector3 spawnPosition = new Vector3 (x, spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate (hazard, spawnPosition, spawnRotation);

				// wait time between calls
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;
			}
		}
	}

	void updateScore() {
		scoreText.text = "Score: " + score;
	}

	public void addScore(int scoreValue) {
		score += scoreValue;
		updateScore ();
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
