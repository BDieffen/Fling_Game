using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    GameManager gameManager;
    public GameObject connectedTo;
    public PaddleScript connectedPaddleScript;
    bool firstShot = true;

	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.nextPaddle = GameObject.Find("Paddle2");
    }
	
	void Update () {
        //Moves the ball with the paddle it is currently attached to (Doesn't need to move with the first shot since the first paddle doesn't move)
        if (!firstShot && connectedPaddleScript != null)
        {
            transform.Translate(connectedPaddleScript.direction * connectedPaddleScript.speed * Time.deltaTime);
        }	
	}

    //Triggers when it hits the Game Over threshold. There are two since it didn't always trigger with the first one
    private void OnTriggerEnter(Collider col)
    {
        /*if (col.gameObject.tag == "KillZone")
        {
            gameManager.GameOver();
        }*/
        if (col.gameObject.name == "BallDestroy")
        {
            gameManager.lives--;
            if (gameManager.lives > 0)
            {
                transform.position = new Vector3(connectedTo.transform.position.x, connectedTo.transform.position.y + .2f, transform.position.z);
            }
            //gameManager.GameOver();
            //Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            gameManager.lives--;
            if(gameManager.lives > 0)
            {
                transform.position = new Vector3(connectedTo.transform.position.x, connectedTo.transform.position.y + .2f, transform.position.z);
            }
        }
        else if(col.gameObject.tag == "PowerUp")
        {
            if(col.gameObject.name == "ExtraLife")
            {
                gameManager.lives++;
                Destroy(col.gameObject);
            }
            else if(col.gameObject.name == "LargerPaddlePower")
            {
                Destroy(col.gameObject);
            }
        }
    }

    //Triggers when it goes above a paddle. Using the exit of a trigger to make it easier to teleport the ball on top of the paddle
    //as well as it just feeling smoother than triggering as soon as it hits
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Paddle")
        {
            connectedTo = other.gameObject;
            connectedPaddleScript = connectedTo.GetComponent<PaddleScript>();
            gameManager.ApplyScore();
            transform.position = new Vector3(transform.position.x, connectedTo.transform.position.y + .2f, transform.position.z);
            firstShot = false;
            CalculateNextPaddle();
        }
    }

    void CalculateNextPaddle()
    {
        switch (connectedTo.gameObject.name)
        {
            case "Paddle1":
                gameManager.nextPaddle = GameObject.Find("Paddle2");
                break;
            case "Paddle2":
                gameManager.nextPaddle = GameObject.Find("Paddle3");
                break;
            case "Paddle3":
                gameManager.nextPaddle = GameObject.Find("Paddle1");
                break;
        }
    }
}
