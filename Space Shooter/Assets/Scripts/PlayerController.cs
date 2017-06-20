using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // needs to be done to be visible in inspector
public class Boundary {

	public float xMin, xMax, zMin, zMax;

}


public class PlayerController : MonoBehaviour {

	public int speed;
	public float tilt;
	public Boundary bound;



	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 direction = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (direction * 3);
		rb.velocity = direction * speed;


		float x = rb.position.x;
		float z = rb.position.z;

		rb.position = new Vector3 (
			Mathf.Clamp(x, bound.xMin, bound.xMax), 
			0.0f, 
			Mathf.Clamp(z, bound.zMin, bound.zMax));

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

	}
}
