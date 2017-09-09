using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelects : MonoBehaviour {

    public GameObject optionsManager;
    PlayerMatOptions matOptions;

	// Use this for initialization
	void Start () {
        matOptions = optionsManager.GetComponent<PlayerMatOptions>();
		
	}

    // Update is called once per frame
    void Update()
    {

        int nbTouches = Input.touchCount;

        //Do something if there is at least 1 touch detected
        if (nbTouches > 0)
        {
            print(nbTouches + " touch(es) detected");

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckTouch(Input.GetTouch(0).position);
            }

        }
    }

    public void CheckTouch(Vector2 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        var hit = Physics2D.OverlapPoint(touchPos);

        if (hit)
        {
            //Debug.Log(hit.transform.gameObject.name);
            //hit.transform.gameObject.SendMessage('Clicked', 0, SendMessageOptions.DontRequireReceiver);
            if (hit.transform.gameObject.name == "BallRight")
            {
                matOptions.BallRight();
            }
            else if (hit.transform.gameObject.name == "BallLeft")
            {
                matOptions.BallLeft();
            }
            else if (hit.transform.gameObject.name == "PaddleRight")
            {
                matOptions.PaddleRight();
            }
            else if (hit.transform.gameObject.name == "PaddleLeft")
            {
                matOptions.PaddleLeft();
            }
            else if (hit.transform.gameObject.name == "BackButton")
            {
                matOptions.BackAndSave();
            }
            else return;
        }
    }

}
