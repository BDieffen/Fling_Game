﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Resources.UnloadUnusedAssets();

    }
	
	// Update is called once per frame
	void Update () {

        int nbTouches = Input.touchCount;

        //Do something if there is at least 1 touch detected
        if (nbTouches > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckTouch(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                TouchEnd(Input.GetTouch(0).position);
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
            if (hit.transform.gameObject.name == "BallRight")
            {
                //matOptions.BallRight();
            }
            else if (hit.transform.gameObject.name == "BallLeft")
            {
                //matOptions.BallLeft();
            }
            else if (hit.transform.gameObject.name == "PaddleRight")
            {
                //matOptions.PaddleRight();
            }
            else if (hit.transform.gameObject.name == "PaddleLeft")
            {
                //matOptions.PaddleLeft();
            }
            else return;
        }
    }

    void TouchEnd(Vector2 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        var hit = Physics2D.OverlapPoint(touchPos);

        if (hit.transform.gameObject.name == "BackButton")
        {
            //matOptions.BackAndSave();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options_Menu");
    }
}
