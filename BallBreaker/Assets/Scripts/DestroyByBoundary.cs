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

		// scoring
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj != null)
			gameController = gameControllerObj.GetComponent<GameController> ();
		else
			Debug.Log ("Cannot find 'GameController' script");
	}

	void OnTriggerExit2D(Collider2D other) {



		// object pool would be better, may be unable with splitting
		Destroy (other.gameObject);

		// add to score
		int scoreValue = other.gameObject.GetComponent<Mover>().GetScore();
		gameController.AddScore(scoreValue);

	}
}
