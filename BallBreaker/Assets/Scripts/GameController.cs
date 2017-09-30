using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {




	public GUIText scoreText;

	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnBalls ());
	}



	// Update is called once per frame
	void Update () {
		CheckInput ();
	}


	void CheckInput() {

		foreach (Touch touch in Input.touches) {
			HandleTouch (touch.fingerId, Camera.main.ScreenToWorldPoint (touch.position), touch.phase);
		}

		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButton (0)) {
				HandleTouch (9, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButtonUp (0)) {
				HandleTouch (8, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {


		bool beginTouch = touchPhase.Equals (TouchPhase.Began) && touchFingerId == 10;
		float radiusOffset = 1.7f;

		if(beginTouch) {
			Vector2 touchPosition2D = new Vector2 (touchPosition.x, touchPosition.y);
			RaycastHit2D rayHit = Physics2D.Raycast (touchPosition2D, new Vector2(0f,0f));


			if (rayHit && rayHit.transform.gameObject.tag.Equals("Ball")) {
				GameObject ball = rayHit.transform.gameObject;
				ball.name = rayHit.transform.gameObject.name; // get ride of (Clone)
				Vector3 rotation = ball.transform.rotation.eulerAngles;

				// shift left and right to avoid collisions on spawn
				float degrees = rotation.z;
				float radians = (degrees * Mathf.PI) / 180;
				float x = Mathf.Cos (radians) * radiusOffset;
				float y = Mathf.Sin (radians) * radiusOffset;

				// left spawn
				Quaternion left = Quaternion.Euler(new Vector3(0f,0f,rotation.z+90));
				Vector2 spawnPositionLeft = new Vector2 (touchPosition.x-x, touchPosition.y+y); // keep spawned on seperate planes (-1 + 1)

				// right spawn
				Quaternion right = Quaternion.Euler(new Vector3(0f,0f,rotation.z+270));
				Vector2 spawnPositionRight = new Vector2 (touchPosition.x+y, touchPosition.y-x);


				ball.transform.position = new Vector2 (100f, 100f);
				Instantiate (ball, spawnPositionLeft, left);
				Instantiate (ball, spawnPositionRight, right);
				Destroy (ball);

			}
		}
	}


	public List<GameObject> balls;
	public int startWait;
	public int gapWait;

	IEnumerator SpawnBalls() {
		yield return new WaitForSeconds (startWait);

		while (true) {

			Vector3 spawnPosition = new Vector2 (0f, 0f);

			Quaternion randomRotation = new Quaternion ();
			randomRotation.eulerAngles = new Vector3 (0f, 0f, Random.Range (0, 360));

			GameObject ball = balls [Random.Range (0, balls.Count)];

			Instantiate (ball, spawnPosition, randomRotation);

			yield return new WaitForSeconds (gapWait);
		}

	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void AddScore(int scoreValue) {
		score += scoreValue;
		UpdateScore ();
	}
		
}
