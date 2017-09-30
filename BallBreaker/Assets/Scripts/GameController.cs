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
		MaintainVelocity ();
	}

	private void MaintainVelocity() {
		foreach(GameObject ball in balls) {
			Vector3 velocity = ball.GetComponent<Rigidbody2D> ().velocity;
			Debug.Log (velocity);
		}
	}

	void CheckInput() {

		foreach (Touch touch in Input.touches) {
			HandleTouch (touch.fingerId, Camera.main.ScreenToWorldPoint (touch.position), touch.phase);
		}

		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
			}
//			if (Input.GetMouseButton (0)) {
//				HandleTouch (9, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
//			}
//			if (Input.GetMouseButtonUp (0)) {
//				HandleTouch (8, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
//			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {


		bool beginTouch = touchPhase.Equals (TouchPhase.Began) && touchFingerId == 10;


		if(beginTouch) {
			Vector2 touchPosition2D = touchPosition;
			RaycastHit2D rayHit = Physics2D.Raycast (touchPosition2D, new Vector2(0f,0f));


			if (rayHit && rayHit.transform.gameObject.tag.Equals("Ball")) {
				GameObject ball = rayHit.transform.gameObject;
				ball.name = rayHit.transform.gameObject.name; // get ride of (Clone)
				splitBall(ball, touchPosition2D);

			}
		}
	}

	private float radiusOffset = 2.0f;

	private void splitBall(GameObject ball, Vector2 touchPosition) {
		Vector3 rotation = ball.transform.rotation.eulerAngles;
		Debug.DrawLine (Vector3.zero, touchPosition, Color.red, 1.0f);

		// needs to -10 because the balls also have a -10 z for some reason??
		Vector3 z_axis = new Vector3 (0, 0, -10);
		Vector2 normal = Vector3.Cross (touchPosition, z_axis);
		Debug.DrawLine (touchPosition, normal, Color.green, 1.0f);

		normal = normal.normalized;

		// spawn positions
		Vector2 spawnPositionLeft = touchPosition + (normal * radiusOffset);
		Quaternion left = Quaternion.Euler(new Vector3(0f,0f,rotation.z+90));

		Vector2 spawnPositionRight = touchPosition + (normal * radiusOffset * -1);
		Quaternion right = Quaternion.Euler(new Vector3(0f,0f,rotation.z+270));


//		// shift left and right to avoid collisions on spawn
//		float degrees = rotation.z;
//		float radians = (degrees * Mathf.PI) / 180;
//		float x = Mathf.Cos (radians) * radiusOffset;
//		float y = Mathf.Sin (radians) * radiusOffset;
//
//		// left spawn
//		Quaternion left = Quaternion.Euler(new Vector3(0f,0f,rotation.z+90));
//		Vector2 spawnPositionLeft = new Vector2 (touchPosition.x-x, touchPosition.y+y); // keep spawned on seperate planes (-1 + 1)
//
//		// right spawn
//		Quaternion right = Quaternion.Euler(new Vector3(0f,0f,rotation.z+270));
//		Vector2 spawnPositionRight = new Vector2 (touchPosition.x+y, touchPosition.y-x);

		// move original ball off screen
		ball.transform.position = new Vector2 (100f, 100f);
		// create new ones
		balls.Add(Instantiate (ball, spawnPositionLeft, left));
		balls.Add(Instantiate (ball, spawnPositionRight, right));
		// remove balls isn't working, causing a reference error
		// https://forum.unity.com/threads/unsure-of-how-to-add-and-remove-gameobjects-from-list-as-they-spawn-die.132624/
		balls.Remove (ball);
		Destroy (ball);
	}


	public List<GameObject> balls;
	public int startWait;
	public int gapWait;

	IEnumerator SpawnBalls() {
		yield return new WaitForSeconds (startWait);

		while (true) {

			Vector2 spawnPosition = Vector2.zero;

			Quaternion randomRotation = Quaternion.identity;
			randomRotation.eulerAngles = new Vector3 (0f, 0f, Random.Range (0, 360));

			GameObject ball = balls [Random.Range (0, balls.Count)];

			balls.Add(Instantiate (ball, spawnPosition, randomRotation));

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
