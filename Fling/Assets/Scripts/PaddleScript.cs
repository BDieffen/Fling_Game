using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    float speed = 0f;
    Vector3 direction;
    //float paddleSize;
    public GameObject teleEntrance;
    public GameObject teleExit;

    // Use this for initialization
    void Start () {
        if(gameObject.name == "Paddle3")
        {
            RandomizeSpeed();
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == teleEntrance.gameObject.name)
        {
            transform.position = teleExit.transform.position;
            RandomizeSpeed();
        }

        if (other.gameObject.name == "BoundRight")
        {
            direction = -direction;
        }
        else if (other.gameObject.name == "BoundLeft")
        {
            direction = -direction;
        }
    }

    void RandomizeSpeed()
    {
        float startDir = Random.Range(-1, 1);
        speed = Random.Range(1, 5);
        if (startDir > 0) {
            direction = new Vector3(1, 0, 0);
        }else direction = new Vector3(-1, 0, 0);
    }

    void RandomizeSize()
    {

    }
}
