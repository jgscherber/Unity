using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	// method in the box, other is the things that hit it
	void OnTriggerExit(Collider other) {
		Destroy (other.gameObject);
	}

}
