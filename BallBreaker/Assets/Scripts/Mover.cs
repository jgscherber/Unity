using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	private Rigidbody2D rb;
    private Transform tf;
    private Camera cam;

	public float startingSpeed;
	public int scoreValue = 1;
    public SplitTypes splitType;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
        tf = GetComponent<Transform>();
        cam = Camera.main;

        // set the size of the balls based on the screen size
        float scale = cam.orthographicSize / 15f;
        tf.localScale = new Vector3(scale,scale);


    }

	// returns the score value of the ball
	public int getScore() {
		return scoreValue;
	}


}
