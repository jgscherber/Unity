using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	public GameController gameController;

	private Camera reference;
	private BoxCollider2D boundingBox;
	private float width;
	private float height;

	void Start() {
		// set the collision detector to be the size of the visible area
		reference = Camera.main;
		height = 2f * reference.orthographicSize;
		width = height * reference.aspect;

		boundingBox = GetComponent<BoxCollider2D> ();
		boundingBox.size = new Vector2 (width, height);

		// need to get reference for scoring scoring
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj != null)
			gameController = gameControllerObj.GetComponent<GameController> ();
		else
			Debug.Log ("Cannot find 'GameController' script");
	}
		
	// used for destroying balls as they move off screen
	void OnTriggerExit2D(Collider2D other) {

        GameObject otherObj = other.gameObject;

		// need to remove from balls array also
		gameController.ballsCreated.Remove(otherObj);

        // add to score only if active (not exiting due to touched)
        if (otherObj.activeSelf)
        {
            int scoreValue = otherObj.GetComponent<Mover>().getScore();
            gameController.AddScore(scoreValue);

            // object pool would be better, may be unable with splitting
            Destroy(otherObj);
        }

        

    }
}
