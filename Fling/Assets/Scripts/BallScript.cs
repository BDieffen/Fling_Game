using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    GameManager gameManager;

    public GameObject connectedTo;

    Vector3 offset;
    bool firstShot = true;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
        offset = new Vector3(connectedTo.transform.position.x + transform.position.x, connectedTo.transform.position.y + .2f, connectedTo.transform.position.z + transform.position.z);


        if (gameManager.canShoot && !firstShot)
        {
            //transform.position = offset;
            transform.position = new Vector3(connectedTo.transform.position.x + transform.position.x, connectedTo.transform.position.y + .2f, connectedTo.transform.position.z + transform.position.z);
        }
		
	}

    public void OnBecameInvisible()
    {
        gameManager.GameOver();
        Destroy(gameObject);
    }

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

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Paddle")
        {
            connectedTo = other.gameObject;
            gameManager.canShoot = false;
            gameManager.score++;
            gameManager.scoreText.text = gameManager.score.ToString();
            gameManager.ballSpeed = 0;
            gameManager.scrolling = true;
            transform.position = new Vector3(transform.position.x, connectedTo.transform.position.y + .2f, transform.position.z);
            offset = new Vector3(connectedTo.transform.position.x + transform.position.x, connectedTo.transform.position.y + .2f, connectedTo.transform.position.z + transform.position.z);
            firstShot = false;

            //gameObject.transform.parent = connectedTo.transform;
        }
    }
}
