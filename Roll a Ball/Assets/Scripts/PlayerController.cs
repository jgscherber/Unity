using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed; // shows up in inspector as an editable property
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start() // run at first frame of the game (capital-S)
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		updateCountText();
		winText.text = "";
	}
	
    void FixedUpdate() // called before physics calculations
    {
		float moveHorizontal = Input.GetAxis ("Horizontal"); // left-right arrow keys
		float moveVertical = Input.GetAxis ("Vertical"); // up-down arrow keys

		Vector3 direction = new Vector3 (moveHorizontal, 0.0f, moveVertical); // y is normal to the plane

		rb.AddForce (direction * speed);

    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			updateCountText();
			if (count >= 10) 
			{
				winText.text = "You win!";
			}
		}
	}

	void updateCountText() 
	{
		countText.text = "Count: " + count.ToString ();

	}

}
