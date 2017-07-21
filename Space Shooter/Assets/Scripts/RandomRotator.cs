using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

	public float tumble = 2.0F;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();

		rb.angularVelocity = Random.insideUnitCircle * tumble;
	}

}
