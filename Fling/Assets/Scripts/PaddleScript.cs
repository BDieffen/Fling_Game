using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    public float speed = 0f;
    public float currentMaxSpeed = 5;
    float minSize = .9f;
    public float currentMinSize = 1.8f;
    float maxSize = 2.5f;
    public float currentMaxSize = 2.5f;
    float currentSize = 2.5f;
    public Vector3 direction;
    //float paddleSize;
    public GameObject teleEntrance;
    public GameObject teleExit;
    GameManager gameScript;
    Vector3 pos;

    // Use this for initialization
    void Start () {
        gameScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameObject.name == "Paddle3")
        {
            RandomizeSpeed(gameScript.score);
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
        pos = Camera.main.ViewportToWorldPoint(transform.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == teleEntrance.gameObject.name)
        {
            transform.position = teleExit.transform.position;
            RandomizeSpeed(gameScript.score);
            if(gameScript.score >= 5)
            {
                RandomizeSize(gameScript.score);
            }
        }

        if (other.gameObject.name == "BoundRight" || other.gameObject.name == "BoundLeft")
        {
            direction = -direction;
        }
        /*else if (other.gameObject.name == "BoundLeft")
        {
            direction = -direction;
        }*/
        /*else if (pos.x < 0 || pos.x > 1)
        {
            direction = -direction;
        }*/
    }

    void RandomizeSpeed(int currentScore)
    {
        float startDir = Random.Range(-1, 1);
        speed = Random.Range(1, currentMaxSpeed);
        if (startDir > 0) {
            direction = new Vector3(1, 0, 0);
        }else direction = new Vector3(-1, 0, 0);
    }

    void RandomizeSize(int currentScore)
    {
        currentSize = Random.Range(currentMinSize, currentMaxSize);
        transform.localScale = new Vector3(currentSize, transform.localScale.y, transform.localScale.z);
    }
}
