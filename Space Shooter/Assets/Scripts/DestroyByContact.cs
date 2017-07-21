using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {


	public GameObject explosion;
	public GameObject playerExplosion;

	void OnTriggerEnter(Collider other) {
		if (!other.tag.Equals ("Boundary")) {
			Destroy (other.gameObject);
			Destroy (gameObject);

			GameObject clone = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
			Destroy (clone, 2.0F);
			if(other.tag.Equals("Player")) {
				clone = Instantiate (playerExplosion, other.transform.position, other.transform.rotation) as GameObject;
				Destroy (clone, 2.0F);
			}
		}

	}
}
