using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    GameManager gameManager;

    GameObject connectedTo;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Paddle")
        {
            connectedTo = other.gameObject;
            //gameObject.transform.parent = connectedTo.transform;

            gameManager.canShoot = false;
            gameManager.score++;
            gameManager.scoreText.text = gameManager.score.ToString();
            gameManager.ballSpeed = 0;
            gameManager.scrolling = true;
            transform.position = new Vector3(transform.position.x, connectedTo.transform.position.y + .2f, transform.position.z);
        }
    }
}
