using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject[] paddles = new GameObject[3];
    GameObject ball;
    BallScript ballScript;

    public float ballSpeed = 0;
    float defBallSpeed = 9;
    float movement = 7.5f;

    float m_distanceTraveled = 0f;

    public bool scrolling = false;

    public bool canShoot = true;

    public int score = 0;

    public int highScore = 0;

    public TextMeshPro scoreText;
    public TextMeshPro highScoreText;
    public GameObject playAgainButton;

    // Use this for initialization
    void Start() {

        playAgainButton.SetActive(false);

        if (File.Exists(Application.persistentDataPath + "/HighScore.dat"))
        {
            Load();
        }

        ball = GameObject.Find("Ball");
        ballScript = ball.GetComponent<BallScript>();
        paddles[0] = GameObject.Find("Paddle1");
        paddles[1] = GameObject.Find("Paddle2");
        paddles[2] = GameObject.Find("Paddle3");

        canShoot = true;
        if (highScore != 0)
        {
            highScoreText.text = "High Score: " + highScore;
        }
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
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!scrolling && canShoot)
            {
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
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
            Save(score);
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
        }

        playAgainButton.SetActive(true);

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Save(int saveHighScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerOptions.dat");

        PlayerData data = new PlayerData();
        data.highScore = saveHighScore;
        //send.theQuotes = quotes;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/HighScore.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            //LoggedQuotes pulled = (LoggedQuotes)bf.Deserialize(file);
            file.Close();

            highScore = data.highScore;
        }
    }

    [Serializable]
    class PlayerData
    {
        public int highScore = 0;
    }
}