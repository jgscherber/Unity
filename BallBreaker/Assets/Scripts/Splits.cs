using System.Collections;
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
        }

    }

    

    static private void splitHalf(GameObject ball, List<GameObject> ballsCreated)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        
        Vector2 position = rb.position;        
        Vector2 direction = rb.velocity.normalized;
        Debug.DrawLine(Vector3.zero, position, Color.red, 1.0f);

        // needs to -10 because the balls also have a -10 z for some reason??
        Vector3 z_axis = new Vector3(0, 0, -10);
        Vector2 normal = Vector3.Cross(direction, z_axis).normalized;        

        Debug.DrawLine(position, normal, Color.green, 1.0f);
        Debug.Log(normal);

        float radiusOffset = ball.GetComponent<Transform>().localScale.x;
        // spawn positions
        Vector2 spawnPositionLeft = position + (normal * radiusOffset);
        Vector2 spawnPositionRight = position + (normal * radiusOffset * -1);


        // move original ball off screen
        ball.transform.position = new Vector2(100f, 100f);

        // create new ones
        GameObject leftBall = (GameObject)Instantiate(ball, spawnPositionLeft, Quaternion.identity);
        GameObject rightBall = (GameObject)Instantiate(ball, spawnPositionRight, Quaternion.identity);

        // assign their velocity
        leftBall.GetComponent<Rigidbody2D>().velocity = normal * ball.GetComponent<Mover>().startingSpeed;
        rightBall.GetComponent<Rigidbody2D>().velocity = normal * -ball.GetComponent<Mover>().startingSpeed;
        Debug.Log(leftBall.GetComponent<Rigidbody2D>().velocity + " " + rightBall.GetComponent<Rigidbody2D>().velocity);

        // add them to our balls list
        ballsCreated.Add(leftBall);
        ballsCreated.Add(rightBall);

        // destroy old ball
        ballsCreated.Remove(ball);
        Destroy(ball);

    } // end splitBall()

    static private void splitQuarters(GameObject ball, List<GameObject> ballsCreated)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        Vector2 position = rb.position;
        Vector2 direction = rb.velocity.normalized;

        // rotate off 45 degrees for spawning 4
        direction = Quaternion.AngleAxis(45.0f, Vector3.forward) * direction;

        Debug.DrawLine(Vector3.zero, position, Color.red, 1.0f);

        // needs to -10 because the balls also have a -10 z for some reason??
        Vector3 z_axis = new Vector3(0, 0, -10);
        Vector2 normal = Vector3.Cross(direction, z_axis).normalized;

        Debug.DrawLine(position, normal, Color.green, 1.0f);
        Debug.Log(normal);

        float radiusOffset = ball.GetComponent<Transform>().localScale.x;
        // spawn positions
        Vector2 spawnPositionLeft = position + (normal * radiusOffset);
        Vector2 spawnPositionRight = position + (normal * radiusOffset * -1);
        Vector2 spawnPositionForward = position + (direction * radiusOffset);
        Vector2 spawnPositionBack = position + (direction * radiusOffset * -1);

        // move original ball off screen
        ball.transform.position = new Vector2(100f, 100f);

        // create new ones
        GameObject leftBall = (GameObject)Instantiate(ball, spawnPositionLeft, Quaternion.identity);
        GameObject rightBall = (GameObject)Instantiate(ball, spawnPositionRight, Quaternion.identity);
        GameObject forwardBall = (GameObject)Instantiate(ball, spawnPositionForward, Quaternion.identity);
        GameObject backBall = (GameObject)Instantiate(ball, spawnPositionBack, Quaternion.identity);

        // assign their velocity
        leftBall.GetComponent<Rigidbody2D>().velocity = normal * ball.GetComponent<Mover>().startingSpeed;
        rightBall.GetComponent<Rigidbody2D>().velocity = normal * -ball.GetComponent<Mover>().startingSpeed;
        forwardBall.GetComponent<Rigidbody2D>().velocity = direction * ball.GetComponent<Mover>().startingSpeed;
        backBall.GetComponent<Rigidbody2D>().velocity = direction * -ball.GetComponent<Mover>().startingSpeed;

        Debug.Log(leftBall.GetComponent<Rigidbody2D>().velocity + " " + rightBall.GetComponent<Rigidbody2D>().velocity);

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

public enum SplitTypes { Half, Quarter };
