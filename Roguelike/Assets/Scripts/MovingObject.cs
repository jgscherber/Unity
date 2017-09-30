using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime = 0.1f;
	public LayerMask blockingLayer; // used for checking colisions

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2D = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime; // can use multiplication, more efficient than division computationally

	}

	protected IEnumerator SmoothMovement(Vector3 end) {
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end,inverseMoveTime * Time.deltaTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
