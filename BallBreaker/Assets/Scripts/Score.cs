using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    static private int score = 0; // static variables exist between scenes, need new class


    static public int getScore() { return score; }
    static public void setScore(int newScore) { score = newScore; }




}
