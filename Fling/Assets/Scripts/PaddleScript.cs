using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    public float speed = 0f;
    public float currentMinSpeed = 1;
    public float currentMaxSpeed = 3;
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
    public bool wasLastPaddle = false;

    // Use this for initialization
    void Start () {
        gameScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameObject.name == "Paddle3")
        {
            RandomizeSpeed();
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
        pos = Camera.main.ViewportToWorldPoint(transform.position);

        if(transform.position.y <= -10)
        {
            TeleportPaddle();
        }
    }

    public void TeleportPaddle()
    {
        wasLastPaddle = false;
        transform.position = teleExit.transform.position;
        RandomizeSpeed();
        if (gameScript.score >= 8)
        {
            RandomizeSize();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.name == teleEntrance.gameObject.name)
        {
            transform.position = teleExit.transform.position;
            RandomizeSpeed(gameScript.score);
            if(gameScript.score >= 5)
            {
                RandomizeSize(gameScript.score);
            }
        }*/

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

    void RandomizeSpeed()
    {
        float startDir = Random.Range(-1, 1);
        speed = Random.Range(currentMinSpeed, currentMaxSpeed);
        if (startDir > 0) {
            direction = new Vector3(1, 0, 0);
        }else direction = new Vector3(-1, 0, 0);
    }

    void RandomizeSize()
    {
        currentSize = Random.Range(currentMinSize, currentMaxSize);
        transform.localScale = new Vector3(currentSize, transform.localScale.y, transform.localScale.z);
    }

    public void DifficultyIncrease(int tier)
    {
        switch (tier)
        {
            case 5:
                currentMinSpeed++;
                break;
            case 10:
                currentMaxSpeed++;
                break;
            case 15:
                currentMinSpeed++;
                currentMinSize--;
                break;
            case 20:
                currentMaxSize--;
                break;
            default:
                break;
        }
    }
}
