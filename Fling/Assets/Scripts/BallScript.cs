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
        if (col.gameObject.tag == "KillZone")
        {
            gameManager.GameOver();
        }
        if (col.gameObject.name == "BallDestroy")
        {
            gameManager.GameOver();
            Destroy(gameObject);
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
        }
    }
}
