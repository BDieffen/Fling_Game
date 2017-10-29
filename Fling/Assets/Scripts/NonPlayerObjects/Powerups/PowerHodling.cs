using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHodling : MonoBehaviour {
    public int[] availablePowers = new int[2];
    public int currentPower;
    public bool isPowerAvailable;

    public bool wasEnlarged = false;
    float enlargedOriginalSize;

    PaddleScript paddleToEdit;
    GameManager gameManagerScript;

    float growthSpeed = 1;
    Vector3 growthScale;

    // Use this for initialization
    void Start () {
        availablePowers[0] = 0;
        availablePowers[1] = 1;
        isPowerAvailable = false;

        gameManagerScript = gameObject.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivatePower()
    {
        if (isPowerAvailable)
        {
            switch (currentPower)
            {
                case 0:
                    paddleToEdit = gameManagerScript.nextPaddle.GetComponent<PaddleScript>();
                    enlargedOriginalSize = paddleToEdit.currentSize;
                    StartCoroutine(EnlargePaddles());
                    break;
                case 1:
                    ExtraBall();
                    break;
            }
            isPowerAvailable = false;
        }
    }

    //When the Enlarge paddle powerup is used, this coroutine smoothly scales up the paddle
    IEnumerator EnlargePaddles()
    {
        growthScale = new Vector3(paddleToEdit.enlargeSize, paddleToEdit.transform.localScale.y, paddleToEdit.transform.localScale.z);

        for (float i=0; i<1; i += Time.deltaTime / growthSpeed)
        {
            paddleToEdit.transform.localScale = Vector3.Lerp(paddleToEdit.transform.localScale, growthScale, i);
            yield return null;
        }
        wasEnlarged = true;
    }

    //When the ball connects with the enlarged paddle, this coroutine smoothly scales the paddle back down
    public IEnumerator ShrinkPaddles(GameObject paddle)
    {
        PaddleScript paddleScript = paddle.GetComponent<PaddleScript>();
        growthScale = new Vector3(paddleScript.currentSize, paddle.transform.localScale.y, paddle.transform.localScale.z);

        for (float i = 0; i < 1; i += Time.deltaTime / growthSpeed)
        {
            paddle.transform.localScale = Vector3.Lerp(paddle.transform.localScale, growthScale, i);
            yield return null;
        }
        wasEnlarged = false;
    }

    void ExtraBall()
    {

    }

    void TriShot()
    {

    }

    //When this powerup is enabled, there is a projection (or indicator) of where the ball will land and when to shoot it 
    //so it lands as close to the center of the paddle as possible.
    void Projection()
    {

    }
}
