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
                    //EnlargePaddles();
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

    /*void EnlargePaddles()
    {
        PaddleScript paddleToEdit = gameManagerScript.nextPaddle.GetComponent<PaddleScript>();
        enlargedOriginalSize = paddleToEdit.currentSize;
        //paddleToEdit.transform.Translate(paddleToEdit.enlargeSize * Time.deltaTime *3, 0, 0);
        paddleToEdit.transform.localScale = new Vector3(paddleToEdit.enlargeSize, paddleToEdit.transform.localScale.y, paddleToEdit.transform.localScale.z);
        wasEnlarged = true;
    }*/
    IEnumerator EnlargePaddles()
    {
        //PaddleScript paddleToEdit = gameManagerScript.nextPaddle.GetComponent<PaddleScript>();
        //enlargedOriginalSize = paddleToEdit.currentSize;
        //paddleToEdit.transform.localScale = new Vector3(paddleToEdit.enlargeSize, paddleToEdit.transform.localScale.y, paddleToEdit.transform.localScale.z);
        while (paddleToEdit.transform.localScale.x < paddleToEdit.enlargeSize)
        {
            paddleToEdit.transform.localScale += new Vector3(1, 0, 0) * Time.deltaTime;
        }
        wasEnlarged = true;
        yield return null;
    }
    void ExtraBall()
    {

    }
}
