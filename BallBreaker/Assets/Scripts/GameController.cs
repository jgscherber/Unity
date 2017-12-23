using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GUIText scoreText;

	private int score;
    private float minSpeed = 10;

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
            
            // if the ball is moving too slow, speed it back up to 10% above minimum
            if (velocity.magnitude < minSpeed)
            {
                velocity = velocity.normalized;
                ball.GetComponent<Rigidbody2D>().velocity = velocity * minSpeed * 1.1f;
            }
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
				Splits.split(ball, touchPosition2D, ballsCreated);

			}
		}
	}

	

	


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
			float speed = ball.GetComponent<Mover> ().startingSpeed;

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
