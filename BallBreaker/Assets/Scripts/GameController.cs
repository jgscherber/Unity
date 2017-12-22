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

	// TODO: currently only logging, need to apply
	private void MaintainVelocity() {
		foreach(GameObject ball in ballsCreated) {
			Vector3 velocity = ball.GetComponent<Rigidbody2D> ().velocity;
			float speed = ball.gameObject.GetComponent<Mover> ().speed;

			velocity = velocity.normalized;
			ball.GetComponent<Rigidbody2D> ().velocity = velocity * speed;

			Debug.Log (ball.GetComponent<Rigidbody2D> ().velocity);
		}
	} // end MaintainVelocity()

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

	// used for treating mouse-clicks as touchs
	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {

		bool beginTouch = touchPhase.Equals (TouchPhase.Began);

		if(beginTouch) {
			Vector2 touchPosition2D = touchPosition;
			RaycastHit2D rayHit = Physics2D.Raycast (touchPosition2D, new Vector2(0f,0f));


			if (rayHit && rayHit.transform.gameObject.tag.Equals("Ball")) {
				// get the ball that was hit
				GameObject ball = rayHit.transform.gameObject;

				// get ride of (Clone)
				ball.name = rayHit.transform.gameObject.name; 
				splitBall(ball, touchPosition2D);

			}
		}
	}

	private float radiusOffset = 2.5f;

	private void splitBall(GameObject ball, Vector2 touchPosition) {
		Vector3 rotation = ball.transform.rotation.eulerAngles;
		Debug.DrawLine (Vector3.zero, touchPosition, Color.red, 1.0f);

		// needs to -10 because the balls also have a -10 z for some reason??
		Vector3 z_axis = new Vector3 (0, 0, -10);
		Vector2 normal = Vector3.Cross (touchPosition, z_axis);
		normal = normal.normalized;

		Debug.DrawLine (touchPosition, normal, Color.green, 1.0f);
		Debug.Log (normal);


		// spawn positions
		Vector2 spawnPositionLeft = touchPosition + (normal * radiusOffset);
		Vector2 spawnPositionRight = touchPosition + (normal * radiusOffset * -1);


		// move original ball off screen
		ball.transform.position = new Vector2 (100f,100f);

		// create new ones
		GameObject leftBall = (GameObject) Instantiate (ball, spawnPositionLeft, Quaternion.identity);
		GameObject rightBall = (GameObject) Instantiate (ball, spawnPositionRight, Quaternion.identity);

		// assign their velocity
		leftBall.GetComponent<Rigidbody2D> ().velocity = normal * ball.GetComponent<Mover> ().speed;
		rightBall.GetComponent<Rigidbody2D> ().velocity = normal * -ball.GetComponent<Mover> ().speed;
		Debug.Log (leftBall.GetComponent<Rigidbody2D> ().velocity + " " + rightBall.GetComponent<Rigidbody2D> ().velocity);

		// add them to our balls list
		ballsCreated.Add(leftBall);
		ballsCreated.Add(rightBall);

		// destroy old ball
		ballsCreated.Remove (ball);
		Destroy (ball);

	} // end splitBall()


	public List<GameObject> ballsCreated;
	public List<GameObject> balls;
	public int startWait;
	public int gapWait;

	IEnumerator SpawnBalls() {
		yield return new WaitForSeconds (startWait);

		while (true) {

			Vector2 spawnPosition = Vector2.zero;

//			Quaternion randomRotation = Quaternion.identity;
//			randomRotation.eulerAngles = new Vector3 (0f, 0f, Random.Range (0, 360));

			// chose one of the ball types to be created (why is this being double used for ball types and tracking!)
			GameObject ball = balls [Random.Range (0, balls.Count)];
			float speed = ball.GetComponent<Mover> ().speed;

			GameObject ballTemp = (GameObject) Instantiate (ball, spawnPosition, Quaternion.identity);
			ballTemp.GetComponent<Rigidbody2D> ().velocity = (new Vector2 (Random.Range (-100,100), Random.Range (-100,100) )).normalized * speed;

			// hold a references
			ballsCreated.Add(ballTemp);

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
