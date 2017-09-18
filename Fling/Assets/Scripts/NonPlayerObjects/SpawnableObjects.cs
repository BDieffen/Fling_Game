using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObjects : MonoBehaviour {

    GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManagerScript.scrolling)
        {
            ScrollObj();
        }
	}

    void ScrollObj()
    {
        transform.Translate(Vector3.down * gameManagerScript.movement * Time.deltaTime);
    }
}
