using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargePaddleScript : MonoBehaviour {

    GameManager gameManagerScript;
    PowerHodling powerManager;

    // Use this for initialization
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        powerManager = GameObject.Find("GameManager").GetComponent<PowerHodling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.scrolling)
        {
            ScrollObj();
        }
        if (transform.position.y <= -4.5)
        {
            Destroy(gameObject);
        }
    }

    void ScrollObj()
    {
        transform.Translate(Vector3.down * gameManagerScript.movement * Time.deltaTime);
    }

    public void ApplyPower()
    {
        powerManager.currentPower = 0;
        powerManager.isPowerAvailable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "ball")
        {
            ApplyPower();
            Destroy(gameObject);
        }
    }
}
