using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    public float enlargeSize = 4.3f;
    public float speed = 0f;
    public float currentMinSpeed = 1;
    public float currentMaxSpeed = 1.5f;
    //float minSize = .9f;
    public float currentMinSize = 1.8f;
    //float maxSize = 2.5f;
    public float currentMaxSize = 2.5f;
    float currentSize = 2.5f;
    public Vector3 direction;
    public GameObject teleExit;
    GameManager gameScript;
    public bool wasLastPaddle = false;

    void Start () {
        gameScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        //When the game starts, this starts moving the 3rd paddle (the first one offscreen)
        if (gameObject.name == "Paddle3")
        {
            RandomizeSpeed();
        }
	}
	
	void Update () {

        //Moves the paddle in the correct direction with the randomly determined speed
        transform.Translate(direction * speed * Time.deltaTime);

        //Once the paddle gets to the lowest point offscreen, it triggers TeleportPaddle, moving it to above the screen and randomizing speed and size
        if(transform.position.y <= -10)
        {
            TeleportPaddle();
        }
    }

    //Teleports the paddle to above the screen and randomizes its speed and, if the score is 8 or higher, randomizes its size
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

    //"Bounces" the paddle to the right or left when it collides with the right or left bounds of the screen (using collider triggers right now)
    //Still need to change this so it bounces off the actual edge of the screen
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BoundRight" || other.gameObject.name == "BoundLeft")
        {
            direction = -direction;
        }
    }

    //Randomizes the paddle's speed based on the current capable minimun and maximum speed levels
    //Also randomizes the starting direction of the paddle
    void RandomizeSpeed()
    {
        float startDir = Random.Range(-1, 1);
        speed = Random.Range(currentMinSpeed, currentMaxSpeed);
        if (startDir > 0) {
            direction = new Vector3(1, 0, 0);
        }else direction = new Vector3(-1, 0, 0);
    }

    //Randomizes the paddle's size based on the current capable minimun and maximum sizes
    void RandomizeSize()
    {
        currentSize = Random.Range(currentMinSize, currentMaxSize);
        transform.localScale = new Vector3(currentSize, transform.localScale.y, transform.localScale.z);
    }

    //Triggers whenever the score increases
    //changes the minimum and maximum capable values of the size and speed of paddles once certain score thresholds are met
    public void DifficultyIncrease(int tier)
    {
        if(tier % 10 == 0)
        {
            currentMaxSpeed++;
        }
        if(tier % 15 == 0)
        {
            currentMinSpeed++;
            currentMinSize--;
        }
        if(tier % 20 == 0)
        {
            currentMaxSize -= 1f;
        }
    }
}
