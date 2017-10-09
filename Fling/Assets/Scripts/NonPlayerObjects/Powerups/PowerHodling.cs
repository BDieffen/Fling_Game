using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHodling : MonoBehaviour {
    public int[] availablePowers = new int[2];
    public int currentPower;
    public bool isPowerAvailable;

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
                    EnlargePaddles();
                    break;
                case 1:
                    ExtraBall();
                    break;
            }
            isPowerAvailable = false;
        }
    }

    void EnlargePaddles()
    {
        PaddleScript paddleToEdit = gameManagerScript.nextPaddle.GetComponent<PaddleScript>();
        //paddleToEdit.transform.Translate(paddleToEdit.enlargeScale * Time.deltaTime * 2, 0, 0);

    }
    void ExtraBall()
    {

    }
}
