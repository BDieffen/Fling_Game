﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject[] paddles = new GameObject[3];
    PaddleScript[] pScripts = new PaddleScript[3];
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

    void Start() {
        playAgainButton.SetActive(false);

        //Loads the user's high score if one exists
        if (File.Exists(Application.persistentDataPath + "/HighScore.dat"))
        {
            Load();
        }

        ball = GameObject.Find("Ball");
        ballScript = ball.GetComponent<BallScript>();
        paddles[0] = GameObject.Find("Paddle1");
        paddles[1] = GameObject.Find("Paddle2");
        paddles[2] = GameObject.Find("Paddle3");
        pScripts[0] = paddles[0].GetComponent<PaddleScript>();
        pScripts[1] = paddles[1].GetComponent<PaddleScript>();
        pScripts[2] = paddles[2].GetComponent<PaddleScript>();

        canShoot = true;
        //Displays the high score if one exists
        if (highScore != 0)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
	
	void Update () {
        //Number of touches detected each frame
        int nbTouches = Input.touchCount;

        //Do something if there is at least 1 touch detected
        if (nbTouches > 0)
        {
            print(nbTouches + " touch(es) detected");

            //Cycles through all the touches and displays the position of the touch on the screen
            /*for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                print("Touch index " + touch.fingerId + " detected at position " + touch.position);
            }*/

            //When the screen is touched and the ball can be shot and the paddles are done scrolling, shoots the ball upward
            if (!scrolling && canShoot)
            {
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
            }
        }

        //Shoots the ball forward by clicking left mouse button. USED FOR COMPUTER TESTING
        if (Input.GetButtonDown("Fire1"))
        {
            if (!scrolling && canShoot)
            {
                ballScript.connectedPaddleScript.wasLastPaddle = true;
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
            }
        }

        //Stops moving the ball if it doesn't exist. USED TO PREVENT ERRORS
        if (ball != null)
        {
            ball.transform.Translate(Vector3.up * ballSpeed * Time.deltaTime);
        }

        //Continues the scrolling of paddles until the distance of 7.5 units has been scrolled and then stops all scrolling movement
        if (scrolling)
        {
            if (m_distanceTraveled < 7.5f)
            {
                //Uses oldPosition in m_distanceTraveled to determine the amount of distance that has been traveled during the scrolling
                Vector3 oldPosition = ballScript.connectedTo.transform.position;
                Scroll();
                m_distanceTraveled += Vector3.Distance(oldPosition, ballScript.connectedTo.transform.position);
            }else
            {
                scrolling = false;
                m_distanceTraveled = 0;
                canShoot = true;
            }
        }
    }

    //Handles the scrolling of the ball and paddles
    public void Scroll()
    {
        paddles[0].transform.Translate(Vector3.down * movement * Time.deltaTime);
        paddles[1].transform.Translate(Vector3.down * movement * Time.deltaTime);
        paddles[2].transform.Translate(Vector3.down * movement * Time.deltaTime);
        ball.transform.Translate(Vector3.down * movement * Time.deltaTime);
    }

    //Triggers from BallScript when the ball hits the next paddle
    //Adds 1 to the score, starts the scrolling, and increases difficulty on the PaddleScripts on certain score thresholds
    public void ApplyScore()
    {
        score++;
        canShoot = false;
        scoreText.text = score.ToString();
        ballSpeed = 0;
        scrolling = true;
        pScripts[0].DifficultyIncrease(score);
        pScripts[1].DifficultyIncrease(score);
        pScripts[2].DifficultyIncrease(score);
    }

    //Triggers from BallScript once the ball hits the trigger past the paddle to handle the Game Over state
    public void GameOver()
    {
        scrolling = false;
        canShoot = false;

        //If the player gets a new high score, this updates (saves) the current high score in both the current state as well as the loadable file
        if(score > highScore)
        {
            Save(score);
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
        }

        //Turns on the "Play Again" button, allowing the player to restart the game and play again
        playAgainButton.SetActive(true);

    }

    //Triggers when the player hits the "Play Again" button. Reloads the current scene (GameScene)
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Loads the options scene when player presses the options button.
    public void LoadOptions()
    {
        SceneManager.LoadScene("Options_Menu");
    }

    //Triggers when the player gets a new high score. Saves the high score into a file which can be retreived later
    public void Save(int saveHighScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/HighScore.dat");

        PlayerData data = new PlayerData();
        data.highScore = saveHighScore;

        bf.Serialize(file, data);
        file.Close();
    }

    //Triggers when the game is loaded. Retreives the player's high score from the saved file
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/HighScore.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            highScore = data.highScore;
        }
    }

    //The class that is created and retreived when saving and loading the high score file
    [System.Serializable]
    public class PlayerData
    {
        public int highScore = 0;
    }
}