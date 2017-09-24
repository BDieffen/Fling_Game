using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMatOptions : MonoBehaviour
{

    public Material[] ballMats = new Material[8];
    public int ballSelectSpot = 0;
    Material selectedBall;
    public Material[] paddleMats = new Material[8];
    public int paddleSelectSpot = 0;
    Material selectedPaddle;
    public Image[] backgroundImages = new Image[5];

    GameObject ball;
    GameObject paddle;
    GameObject paddle2;
    GameObject paddle3;

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "Options_Menu")
        {
            GrabObjects(2);
        }else if(SceneManager.GetActiveScene().name == "Game_Scene")
        {
            GrabObjects(1);
        }

        if (File.Exists(Application.persistentDataPath + "/playerOptions.dat"))
        {
            Load();
        }
    }

    void Update()
    {
        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //throw new System.NotImplementedException();
        if (arg0.name == "Game_Scene")
        {
            GrabObjects(1);
        } else if (arg0.name == "Options_Menu")
        {
            GrabObjects(2);
        }
    }

    void GrabObjects(int scene)
    {
        if(scene == 1)
        {
            ball = GameObject.Find("Ball");
            ball.GetComponent<MeshRenderer>().material = selectedBall;
            paddle = GameObject.Find("Paddle1");
            paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
            paddle2 = GameObject.Find("Paddle2");
            paddle2.GetComponent<MeshRenderer>().material = selectedPaddle;
            paddle3 = GameObject.Find("Paddle3");
            paddle3.GetComponent<MeshRenderer>().material = selectedPaddle;
        }
        if(scene == 2)
        {
            ball = GameObject.Find("Ball");
            ball.GetComponent<MeshRenderer>().material = selectedBall;
            paddle = GameObject.Find("Paddle1");
            paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
            paddle2 = null;
            paddle3 = null;
        }
    }

    public void BallRight()
    {
        scrollRight(ballMats);
    }

    public void BallLeft()
    {
        scrollLeft(ballMats);
    }

    public void PaddleRight()
    {
        scrollRight(paddleMats);
    }

    public void PaddleLeft()
    {
        scrollLeft(paddleMats);
    }

    public void scrollRight(Material[] array)
    {
        if (array == ballMats)
        {
            if (ballSelectSpot == 7)
            {
                ballSelectSpot = 0;
            }
            else
            {
                ballSelectSpot++;
            }
            selectedBall = ballMats[ballSelectSpot];
            ball.GetComponent<MeshRenderer>().material = selectedBall;
        }
        else if (array == paddleMats)
        {
            if (paddleSelectSpot == 7)
            {
                paddleSelectSpot = 0;
            }
            else
            {
                paddleSelectSpot++;
            }
            selectedPaddle = paddleMats[paddleSelectSpot];
            paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
        }
    }

    public void scrollLeft(Material[] array)
    {
        if (array == ballMats)
        {
            if (ballSelectSpot == 0)
            {
                ballSelectSpot = 7;
            }
            else
            {
                ballSelectSpot--;
            }
            selectedBall = ballMats[ballSelectSpot];
            ball.GetComponent<MeshRenderer>().material = selectedBall;
        }
        else if (array == paddleMats)
        {
            if (paddleSelectSpot == 0)
            {
                paddleSelectSpot = 7;
            }
            else
            {
                paddleSelectSpot--;
            }
            selectedPaddle = paddleMats[paddleSelectSpot];
            paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
        }
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerOptions.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerOptions.dat", FileMode.Open);

            PlayerOptions data = (PlayerOptions)bf.Deserialize(file);
            file.Close();

            ballSelectSpot = data.ballSelect;
            paddleSelectSpot = data.paddleSelect;

            selectedBall = ballMats[ballSelectSpot];
            ball.GetComponent<MeshRenderer>().material = selectedBall;
            selectedPaddle = paddleMats[paddleSelectSpot];
            if (SceneManager.GetActiveScene().name == "Game_Scene")
            {
                paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
                paddle2.GetComponent<MeshRenderer>().material = selectedPaddle;
                paddle3.GetComponent<MeshRenderer>().material = selectedPaddle;
            } else if(SceneManager.GetActiveScene().name == "Options_Menu")
            {
                paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
            }
        }
    }

    public void BackAndSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerOptions.dat");

        PlayerOptions data = new PlayerOptions();
        data.ballSelect = ballSelectSpot;
        data.paddleSelect = paddleSelectSpot;

        bf.Serialize(file, data);
        file.Close();

        SceneManager.LoadScene("Game_Scene");
    }

    [System.Serializable]
    public class PlayerOptions
    {
        public int ballSelect;
        public int paddleSelect;
    }
}
