using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {


	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue = 1;

	private GameController gameController;

	void Start() {
		GameObject gameControllerObj = GameObject.FindWithTag ("GameController");
		if (gameControllerObj != null) {
			gameController = gameControllerObj.GetComponent<GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!other.tag.Equals ("Boundary")) {
			Destroy (other.gameObject);
			Destroy (gameObject);

			GameObject clone = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
			Destroy (clone, 2.0F);
			if(other.tag.Equals("Player")) {
				clone = Instantiate (playerExplosion, other.transform.position, other.transform.rotation) as GameObject;
				Destroy (clone, 2.0F);
				gameController.GameOver ();
			}

			gameController.addScore (scoreValue);
		}

	}


}
