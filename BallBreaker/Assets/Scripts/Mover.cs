using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int scoreValue = 1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		// maybe change this to a set velocity (where does the random direction come in?)
		rb.AddForce(transform.up * speed);
	}

	// returns the score value of the ball
	public int GetScore() {
		return scoreValue;
	}
}
