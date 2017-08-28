using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    GameManager gameManager;

    public GameObject connectedTo;
    public PaddleScript connectedPaddleScript;

    Vector3 offset;
    bool firstShot = true;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
        //offset = new Vector3(connectedTo.transform.position.x + transform.position.x, connectedTo.transform.position.y + .2f, connectedTo.transform.position.z + transform.position.z);


        if (!firstShot && connectedPaddleScript != null)
        {
            //transform.position = offset;
            transform.Translate(connectedPaddleScript.direction * connectedPaddleScript.speed * Time.deltaTime);
        }
		
	}

    /*public void OnBecameInvisible()
    {
        gameManager.GameOver();
        Destroy(gameObject);
    }*/

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
            connectedPaddleScript = connectedTo.GetComponent<PaddleScript>();
            gameManager.ApplyScore();
            transform.position = new Vector3(transform.position.x, connectedTo.transform.position.y + .2f, transform.position.z);
            offset = new Vector3(connectedTo.transform.position.x + transform.position.x, connectedTo.transform.position.y + .2f, connectedTo.transform.position.z + transform.position.z);
            firstShot = false;

            //gameObject.transform.parent = connectedTo.transform;
        }
    }
}
