using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastGamePoints : MonoBehaviour {

    private Text totalPoints;

    private Camera reference;
    private float height;


    // Use this for initialization
    void Start () {
        reference = Camera.main;
        height = 2f * reference.orthographicSize;
        totalPoints = GetComponent<Text>();

        totalPoints.fontSize = (int)height;
        totalPoints.text = "" + Score.getScore() + " Points";
	}
	
}
