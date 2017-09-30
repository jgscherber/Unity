using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;

	public static GameManager instance = null;

	private int level = 4;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame() {
		boardScript.SetupScene (level);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
