using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //These are for framerate testing
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /*public TextMeshPro framerate;
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;*/
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    GameObject[] paddles = new GameObject[3];
    PaddleScript[] pScripts = new PaddleScript[3];
    public GameObject nextPaddle;
    GameObject ball;
    BallScript ballScript;
    PowerHodling powerupManager;

    public float ballSpeed = 0;
    float defBallSpeed = 9;
    public float movement = 7.5f;

    float m_distanceTraveled = 0f;

    public bool scrolling = false;

    public bool canShoot = true;

    public int score = 0;

    public int highScore = 0;

    public int lives = 1;

    public TextMeshPro scoreText;
    public TextMeshPro highScoreText;
    public GameObject playAgainButton;
    public GameObject goToOptions;

    ObjectSpawning objectSpawnerScript;
    bool canSpawnPowers = false;

    void Start() {
        Resources.UnloadUnusedAssets();
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

        goToOptions = GameObject.Find("GoToOptions");
        powerupManager = gameObject.GetComponent<PowerHodling>();

        canShoot = true;
        //Displays the high score if one exists
        if (highScore != 0)
        {
            highScoreText.text = "High Score: " + highScore;
        }

        objectSpawnerScript = GetComponent<ObjectSpawning>();
        canSpawnPowers = false;
    }

    void Update() {
        //This section displays current framerate. Use this for optimization testing
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /*if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }

        framerate.text = m_lastFramerate.ToString();*/
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //If lives is equal to or less than 0 then activate game over
        if (lives <= 0)
        {
            GameOver();
        }

        //Number of touches detected each frame
        int nbTouches = Input.touchCount;

        //Do something if there is at least 1 touch detected
        if (nbTouches > 0 && !scrolling)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckTouch(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                TouchEnd(Input.GetTouch(0).position);
            }



            //When the screen is touched and the ball can be shot and the paddles are done scrolling, shoots the ball upward
            /*if (!scrolling && canShoot)
            {
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
            }*/
        }
        //Shoots the ball forward by clicking left mouse button. USED FOR COMPUTER TESTING
        /*if (Input.GetButtonDown("Fire1"))
        {
            if (!scrolling && canShoot)
            {
                ballScript.connectedPaddleScript.wasLastPaddle = true;
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
            }
        }*/

        //Stops moving the ball if it doesn't exist. USED TO PREVENT ERRORS
        if (ball != null)
        {
            //Only applies movement to the ball if the player has shot the ball
            if (!canShoot)
            {
                ball.transform.Translate(Vector3.up * ballSpeed * Time.deltaTime);
            }
        }

        //Continues the scrolling of paddles until the distance of 7.5 units has been scrolled and then stops all scrolling movement
        if (scrolling)
        {
            if (ballScript.connectedTo.transform.position.y > -2.5)//if (m_distanceTraveled < 7.5f)
            {
                //Uses oldPosition in m_distanceTraveled to determine the amount of distance that has been traveled during the scrolling
                //Vector3 oldPosition = ballScript.connectedTo.transform.position;
                Scroll();
                //m_distanceTraveled += Vector3.Distance(oldPosition, ballScript.connectedTo.transform.position);
            }
            else
            {
                scrolling = false;
                //m_distanceTraveled = 0;
                canShoot = true;
                AfterScrolling();
            }
        }
    }

    /*private void FixedUpdate()
    {
        //Stops moving the ball if it doesn't exist. USED TO PREVENT ERRORS
        if (ball != null)
        {
            //Only applies movement to the ball if the player has shot the ball
            if (!canShoot)
            {
                ball.transform.Translate(Vector3.up * ballSpeed * Time.deltaTime);
            }
        }

        //Continues the scrolling of paddles until the distance of 7.5 units has been scrolled and then stops all scrolling movement
        if (scrolling)
        {
            if (ballScript.connectedTo.transform.position.y > -2.5)//if (m_distanceTraveled < 7.5f)
            {
                //Uses oldPosition in m_distanceTraveled to determine the amount of distance that has been traveled during the scrolling
                //Vector3 oldPosition = ballScript.connectedTo.transform.position;
                Scroll();
                //m_distanceTraveled += Vector3.Distance(oldPosition, ballScript.connectedTo.transform.position);
            }
            else
            {
                scrolling = false;
                //m_distanceTraveled = 0;
                canShoot = true;
            }
        }
    }*/

    public void CheckTouch(Vector2 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        var hit = Physics2D.OverlapPoint(touchPos);

        if (hit)
        {
            if (hit.transform.gameObject.name == "GoToOptions")
            {
                LoadOptions();
            }
            if (hit.transform.gameObject.name == "CurrentPowerUp")
            {
                powerupManager.ActivatePower();
            }
        }
        else
        {
            //When the screen is touched and the ball can be shot and the paddles are done scrolling, shoots the ball upward
            if (!scrolling && canShoot)
            {
                ballScript.connectedPaddleScript = null;
                ballScript.connectedTo = null;
                canShoot = false;
                ballSpeed = defBallSpeed;
            }
        }
    }

    void TouchEnd(Vector2 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        var hit = Physics2D.OverlapPoint(touchPos);

        if (hit.transform.gameObject.name == "GoToOptions")
        {
            LoadOptions();
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

    //Gets called every time the scrolling stops
    public void AfterScrolling()
    {
        //Activates the spawning of powerups
        if (!canSpawnPowers && score == 19)
        {
            canSpawnPowers = true;
            objectSpawnerScript.SpawnPower();
        }
        if (canSpawnPowers)
        {
            if((score + 1) % 10 == 0)
            {
                objectSpawnerScript.SpawnPower();
            }
        }

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