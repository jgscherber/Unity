﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splits : MonoBehaviour {

    

    static public void split(GameObject ball, Vector2 touchPosition, List<GameObject> ballsCreated)
    {
        SplitTypes version = ball.GetComponent<Mover>().splitType;

        switch(version)
        {
            case SplitTypes.Half:
                splitHalf(ball, ballsCreated);
                break;
            case SplitTypes.Quarter:
                splitQuarters(ball, ballsCreated);
                break;

            case SplitTypes.Reverse:
                reverseDirection(ball, ballsCreated);
                break;
        }

    }

    static private void reverseDirection(GameObject ball, List<GameObject> ballsCreated)
    {

    }

    static private void splitHalf(GameObject ball, List<GameObject> ballsCreated)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        
        Vector2 position = rb.position;        
        Vector2 direction = rb.velocity.normalized;


        // needs to -10 because the balls also have a -10 z for some reason??
        Vector3 z_axis = new Vector3(0, 0, -10);
        Vector2 normal = Vector3.Cross(direction, z_axis).normalized;        

        

        float radiusOffset = ball.GetComponent<Transform>().localScale.x;
        // spawn positions
        Vector2 spawnPositionLeft = position + (normal * radiusOffset);
        Vector2 spawnPositionRight = position + (normal * radiusOffset * -1);

        // TODO: do this without adding points
        // move original ball off screen
        
        ball.SetActive(false);

        // create new ones
        GameObject leftBall = (GameObject)Instantiate(ball, spawnPositionLeft, Quaternion.identity);
        GameObject rightBall = (GameObject)Instantiate(ball, spawnPositionRight, Quaternion.identity);

        // activate them
        leftBall.SetActive(true);
        rightBall.SetActive(true);

        // assign their velocity
        leftBall.GetComponent<Rigidbody2D>().velocity = normal * ball.GetComponent<Mover>().startingSpeed;
        rightBall.GetComponent<Rigidbody2D>().velocity = normal * -ball.GetComponent<Mover>().startingSpeed;
        
        // add them to our balls list
        ballsCreated.Add(leftBall);
        ballsCreated.Add(rightBall);

        // destroy old ball
        ballsCreated.Remove(ball);
        Destroy(ball);

    } // end splitHalf()

    static private void splitQuarters(GameObject ball, List<GameObject> ballsCreated)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        Vector2 position = rb.position;
        Vector2 direction = rb.velocity.normalized;

        // rotate off 45 degrees for spawning 4
        direction = Quaternion.AngleAxis(45.0f, Vector3.forward) * direction;

        // needs to -10 because the balls also have a -10 z for some reason??
        Vector3 z_axis = new Vector3(0, 0, -10);
        Vector2 normal = Vector3.Cross(direction, z_axis).normalized;

        

        float radiusOffset = ball.GetComponent<Transform>().localScale.x;
        // spawn positions
        Vector2 spawnPositionLeft = position + (normal * radiusOffset);
        Vector2 spawnPositionRight = position + (normal * radiusOffset * -1);
        Vector2 spawnPositionForward = position + (direction * radiusOffset);
        Vector2 spawnPositionBack = position + (direction * radiusOffset * -1);

        // de-activate original ball to remove from screen
        ball.SetActive(false);

        // create new ones
        GameObject leftBall = (GameObject)Instantiate(ball, spawnPositionLeft, Quaternion.identity);
        GameObject rightBall = (GameObject)Instantiate(ball, spawnPositionRight, Quaternion.identity);
        GameObject forwardBall = (GameObject)Instantiate(ball, spawnPositionForward, Quaternion.identity);
        GameObject backBall = (GameObject)Instantiate(ball, spawnPositionBack, Quaternion.identity);

        // re-activate new balls
        leftBall.SetActive(true);
        rightBall.SetActive(true);
        forwardBall.SetActive(true);
        backBall.SetActive(true);


        // assign their velocity
        leftBall.GetComponent<Rigidbody2D>().velocity = normal * ball.GetComponent<Mover>().startingSpeed;
        rightBall.GetComponent<Rigidbody2D>().velocity = normal * -ball.GetComponent<Mover>().startingSpeed;
        forwardBall.GetComponent<Rigidbody2D>().velocity = direction * ball.GetComponent<Mover>().startingSpeed;
        backBall.GetComponent<Rigidbody2D>().velocity = direction * -ball.GetComponent<Mover>().startingSpeed;

        // spin them!
        float torque = 500.0f;
        leftBall.GetComponent<Rigidbody2D>().AddTorque(torque);
        rightBall.GetComponent<Rigidbody2D>().AddTorque(-torque);
        forwardBall.GetComponent<Rigidbody2D>().AddTorque(-torque);
        backBall.GetComponent<Rigidbody2D>().AddTorque(torque);

        // add them to our balls list
        ballsCreated.Add(leftBall);
        ballsCreated.Add(rightBall);
        ballsCreated.Add(forwardBall);
        ballsCreated.Add(backBall);

        // destroy old ball
        ballsCreated.Remove(ball);
        Destroy(ball);

    } // end splitQuarters()

} // end Class

public enum SplitTypes { Half, Quarter, Reverse };
