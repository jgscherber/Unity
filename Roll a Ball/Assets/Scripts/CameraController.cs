using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position; // only run once at start
	}
	
	// LateUpdate is called once per frame, but after everything in Update() has run
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
