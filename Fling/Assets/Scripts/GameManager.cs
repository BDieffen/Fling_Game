using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject[] paddles = new GameObject[3];
    GameObject ball;

    public float ballSpeed = 0;

    float movement = 7.5f;

    float m_distanceTraveled = 0f;

    public bool scrolling = false;

    public bool canShoot = true;

    public int score = 0;

    public int highScore = 0;

    public TextMeshPro scoreText;

    string filePath;

    // Use this for initialization
    void Start() {

        if (File.Exists(filePath + "/HighScore.dat"))
        {
            Load();
        }

        ball = GameObject.Find("Ball");
        paddles[0] = GameObject.Find("Paddle1");
        paddles[1] = GameObject.Find("Paddle2");
        paddles[2] = GameObject.Find("Paddle3");

        canShoot = true;
    }
	
	// Update is called once per frame
	void Update () {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            print(nbTouches + " touch(es) detected");

            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                print("Touch index " + touch.fingerId + " detected at position " + touch.position);
            }

            if (!scrolling && canShoot)
            {
                //ball.transform.parent = null;
                canShoot = false;
                ballSpeed = 7;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!scrolling && canShoot)
            {
                //ball.transform.parent = null;
                canShoot = false;
                ballSpeed = 7;
            }
        }

        if (ball != null)
        {
            ball.transform.Translate(Vector3.up * ballSpeed * Time.deltaTime);
        }

        if (scrolling)
        {
            if (m_distanceTraveled < 7.5f)
            {
                Vector3 oldPosition = ball.transform.position;
                Scroll();
                m_distanceTraveled += Vector3.Distance(oldPosition, ball.transform.position);
            }else
            {
                scrolling = false;
                m_distanceTraveled = 0;
                canShoot = true;
            }
        }
    }

    public void Scroll()
    {

        paddles[0].transform.Translate(Vector3.down * movement * Time.deltaTime);
        paddles[1].transform.Translate(Vector3.down * movement * Time.deltaTime);
        paddles[2].transform.Translate(Vector3.down * movement * Time.deltaTime);
        ball.transform.Translate(Vector3.down * movement * Time.deltaTime);
    }

    public void GameOver()
    {
        scrolling = false;
        canShoot = false;

        if(score > highScore)
        {
            Save(highScore);
        }
    }

    public void Save(int saveHighScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath + "/playerOptions.dat");

        int newHighScore = saveHighScore;
        //send.theQuotes = quotes;

        bf.Serialize(file, newHighScore);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath + "/HighScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath + "/HighScore.dat", FileMode.Open);

            int pulledHighScore = (int)bf.Deserialize(file);
            //LoggedQuotes pulled = (LoggedQuotes)bf.Deserialize(file);
            file.Close();

            highScore = pulledHighScore;
        }
    }
}