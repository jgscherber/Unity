using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // for end game

public class GameController : MonoBehaviour {

	public Text scoreText;
    public Text timerText;

    public int gameTime = 30; // game time in seconds

	
    private int timeSeconds;
    private float minSpeed = 10;

    private Camera reference;
    private float height;


    // Use this for initialization
    void Start () {

        // re-size text to fit screen
        reference = Camera.main;
        height = 2f * reference.orthographicSize;
        scoreText.fontSize = (int)height;
        timerText.fontSize = (int)height;

        // setup
        Score.setScore(0);
        timeSeconds = 0;

        updateTime();
		UpdateScore ();

        Invoke("AddTime", 1f);
		StartCoroutine (SpawnBalls ());
	}



	// Update is called once per frame
	void Update () {
		CheckInput ();
		MaintainVelocity ();
	}

	
	private void MaintainVelocity() {
		foreach(GameObject ball in ballsCreated) {

			Vector3 velocity = ball.GetComponent<Rigidbody2D> ().velocity;
            
            // if the ball is moving too slow, speed it back up to 10% above minimum
            if (velocity.magnitude < minSpeed)
            {
                velocity = velocity.normalized;
                ball.GetComponent<Rigidbody2D>().velocity = velocity * minSpeed * 1.1f;
            }

		}
	} // end MaintainVelocity()

	void CheckInput() {

		foreach (Touch touch in Input.touches) {
			HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
		}

		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown (0)) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
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
	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {

        // deltaTime in seconds -- not the way to handle, need time and position
		bool beginTouch = touchPhase.Equals (TouchPhase.Began);



		if(beginTouch) {
			Vector2 touchPosition2D = touchPosition;

            Debug.DrawRay(Vector3.zero, touchPosition, Color.blue, 1.0f);

            // have to check all hits to make sure bounding box isn't the only passed
			RaycastHit2D[] allRayHits = Physics2D.RaycastAll (touchPosition2D, new Vector3(0,0,-10));

            
            // TODO: add enemy objects that shouldn't be hit and reduce points            
            foreach (RaycastHit2D rayHit in allRayHits) {

                // need the tag check to avoid passing the bounding box
                if (rayHit && rayHit.transform.gameObject.tag.Equals("Ball")) {

                    // get the ball that was hit
                    GameObject ball = rayHit.transform.gameObject;


                    Splits.split(ball, touchPosition2D, ballsCreated);

                }
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
		scoreText.text = "Score: " + Score.getScore();
	}

    // game times will always be less than 1 minute, can display seconds directly
    void updateTime()
    {
        timerText.text = "" + (gameTime - timeSeconds);

    }

    void AddTime()
    {
        timeSeconds += 1;
        if(gameTime - timeSeconds > 0)
        {
            updateTime();
            Invoke("AddTime", 1f);
        } else
        {
            SceneManager.LoadScene(2);
        }

    }

	public void AddScore(int scoreValue) {
        int score = Score.getScore();
        Score.setScore(score + scoreValue);
		UpdateScore ();
	}
		
}
