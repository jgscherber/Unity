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
		rb.AddForce(transform.up * speed);
	}

	public int GetScore() {
		return scoreValue;
	}
}
